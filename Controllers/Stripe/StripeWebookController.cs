using Stripe;
using Microsoft.AspNetCore.Mvc;
using AirAtlasPro.Data;
using System.Text.Json;
using AirAtlasPro.Models.Stripe;

namespace AirAtlasPro.Controllers.Stripe
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebookController : ControllerBase
    {
        private readonly ApiDbContext _apiDbContext;
        private readonly string _endpointSecret;

        public StripeWebookController(ApiDbContext apiDbContext, IConfiguration configuration)
        {
            _apiDbContext = apiDbContext;
            _endpointSecret = configuration["Stripe:WebhookSecret"]!;
        }

        // POST api/StripeWWebhook
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], _endpointSecret);

            // Handle the event
            switch (stripeEvent.Type)
            {
                case Events.CustomerSubscriptionCreated:
                    break;
                case Events.InvoicePaymentSucceeded:

                    //FileWriter.AppendToJsonLog(json: json);

                    // convert to custom StripeInvoice object
                    StripeInvoice stripeInvoice = JsonSerializer.Deserialize<StripeInvoice>(json)!;

                    // datum
                    List<Datum> lineDatas = stripeInvoice?.Data?.ObjectData?.Lines?.Data!;

                    StripeBgTaskQueue bgTask = new StripeBgTaskQueue()
                    {
                        Id = 0,
                        EmailAddress = stripeInvoice?.Data?.ObjectData?.CustomerEmail ?? null,
                        PhoneNumber = stripeInvoice?.Data?.ObjectData?.CustomerPhone ?? null,
                        Address1 = stripeInvoice?.Data?.ObjectData?.CustomerAddress?.Line1 ?? null,
                        Address2 = stripeInvoice?.Data?.ObjectData?.CustomerAddress?.Line2 ?? null,
                        City = stripeInvoice?.Data?.ObjectData?.CustomerAddress?.City ?? null,
                        State = stripeInvoice?.Data?.ObjectData?.CustomerAddress?.State ?? null,
                        PostalCode = stripeInvoice?.Data?.ObjectData?.CustomerAddress?.PostalCode ?? null,
                        Country = stripeInvoice?.Data?.ObjectData?.CustomerAddress?.Country ?? null,
                        Created = DateTime.UtcNow,
                        SubscriptionStartDate = DateTimeOffset.FromUnixTimeSeconds((long)stripeInvoice?.Data?.ObjectData?.PeriodStart!).DateTime,
                        SubscriptionExpireDate = DateTimeOffset.FromUnixTimeSeconds((long)stripeInvoice?.Data?.ObjectData?.PeriodEnd!).DateTime,
                        EventId = stripeInvoice?.Id ?? null,
                        ProductId = lineDatas[0].Plan?.Product ?? null,
                        CustomerId = stripeInvoice?.Data?.ObjectData?.Customer ?? null,
                        BillingReason = stripeInvoice?.Data?.ObjectData?.BillingReason ?? null,
                        Status = Models.Constants.StripeBgTaskStatus.QUEUED,
                    };
                    try
                    {
                        await _apiDbContext.StripeBgTaskQueues!.AddAsync(bgTask);
                        await _apiDbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        FileWriter.AppendToJsonLog(ex.Message);
                        if (ex.InnerException != null)
                        {
                            FileWriter.AppendToJsonLog($"Inner Exception: {ex.InnerException.Message}");
                        }
                        return BadRequest();
                    }
                    break;
                default:
                    break;
            }
            return Ok();
        }
    }

    public class FileWriter
    {
        public static void AppendToJsonLog(string json)
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string logFilePath = Path.Combine(projectDirectory, "json.log");

            try
            {
                using (StreamWriter writer = System.IO.File.AppendText(logFilePath))
                {
                    writer.WriteLine(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
