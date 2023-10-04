using System.Text.Json.Serialization;

namespace AirAtlasPro.Models.Stripe
{
    public partial class Request
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("idempotency_key")]
        public string? IdempotencyKey { get; set; }
    }
}

