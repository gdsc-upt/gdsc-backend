using GdscBackend.Models;
using Microsoft.Extensions.Configuration;

namespace GdscBackend.Utils.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly IConfiguration _configuration;

        public WebhookService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void SendContact(ContactModel contact)
        {
            var webhook = new Webhook(_configuration["Webhooks:Contact"], "Baiatu' cu contactele",
                "https://www.pngitem.com/pimgs/m/156-1568414_book-contact-icon-volkswagen-hd-png-download.png");
            await webhook.Send(ContactContentBuilder(contact.Name, contact.Email, contact.Subject, contact.Text));
        }

        private string ContactContentBuilder(string author, string mail, string subject, string message)
        {
            return "**Name:**  " + author + "\n" + "**Email:**  " + mail + "\n" + "**Subject:**  " + subject + "\n" +
                   "**Message:**  " + message + "\n";
        }
    }
}