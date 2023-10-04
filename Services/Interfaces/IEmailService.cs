namespace AirAtlasPro.Services.Interfaces
{
	/// <summary>
	/// Send an email
	/// </summary>
	public interface IEmailService
	{
		/// <summary>
		/// Send an email in HTML format
		/// </summary>
		/// <param name="to">Recipient email address</param>
		/// <param name="subject">Subject line</param>
		/// <param name="html">Html formatted body of email</param>
		/// <param name="from">[OPTIONAL] The sender's email, should be configured in appsettings.json file</param>
		/// <returns></returns>
		Task SendHtmlEmailAsync(string to, string subject, string html, string? from = null);
	}
}

