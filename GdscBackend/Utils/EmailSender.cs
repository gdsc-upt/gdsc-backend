using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace GdscBackend.Utils
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _sender;
        private readonly string _senderName;

        public EmailSender(IConfiguration configuration)
        {
            _senderName = configuration["Email:Login"];
            _sender = new SmtpClient(configuration["Email:Host"])
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    configuration["Email:Login"],
                    configuration["Email:Password"]
                ),
                Port = int.Parse(configuration["Email:Port"]),
                EnableSsl = true
            };
        }

        public void SendEmail(string to, string subject, string body)
        {
            _sender.SendAsync(new MailMessage(_senderName, to, subject, body), null);
        }
    }
}