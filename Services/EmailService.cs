using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using AirAtlasPro.Services.Interfaces;

namespace AirAtlasPro.Services
{
    public class EmailService : IEmailService
	{
        private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
            _configuration = configuration;
		}

        public async Task SendHtmlEmailAsync(string to, string subject, string html, string? from = null)
        {
            // create email
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _configuration["MailerSend:Smtp:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                host: _configuration["MailerSend:Smtp:SmtpHost"],
                port: int.Parse(_configuration["MailerSend:Smtp:Port"]!),
                options: SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                userName: _configuration["MailerSend:Smtp:SmtpUser"],
                password: _configuration["MailerSend:Smtp:SmtpPass"]);

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}

