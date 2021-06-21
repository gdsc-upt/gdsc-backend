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
        
        private string ContactContentBuilder(string author, string mail, string subject, string message)
        {
            return "**Name:**  " + author + "\n" + "**Email:**  " + mail + "\n" + "**Subject:**  " + subject + "\n" + "**Message:**  " + message + "\n";
        }

        private string IdeaContentBuilder(string name, string email, string branch, string year, string description)
        {
            return "**Name:**  " + name + "\n" + "**Email:**  " + email + "\n" + "**Year:**  " + year + "\n" +
                   "**Branch:**  " + branch + "\n" + "**Description:\n\"**" + description + "**\"**";
        }

        public async void SendContact(ContactModel contact)
        {
            var webhook = new Webhook(_configuration["Webhooks:Contact"], "Baiatu' cu contactele", "https://www.pngitem.com/pimgs/m/156-1568414_book-contact-icon-volkswagen-hd-png-download.png");
            await webhook.Send(ContactContentBuilder(contact.Name, contact.Email, contact.Subject, contact.Text));
        }

        public async void SendIdea(IdeaModel idea)
        {
            var webhook = new Webhook(_configuration["Webhooks:Ideas"], "Baiatu' cu idei", "https://img.pixers.pics/pho_wat(s3:700/FO/68/19/71/36/700_FO68197136_a0e99a06f6fa463d7c30a941b0e1b2dc.jpg,700,700,cms:2018/10/5bd1b6b8d04b8_220x50-watermark.png,over,480,650,jpg)/stickers-light-bulb-icon.jpg.jpg");
            await webhook.Send(IdeaContentBuilder(idea.Name, idea.Email, idea.Branch, idea.Year.ToString(), idea.Description));
        }
    }
}