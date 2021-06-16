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

        public async void SendContact(string author, string mail, string subject, string message)
        {
            var webhook = new Webhook(_configuration["Webhooks:Contact"]);
            await webhook.Send(author, mail, subject, message);
        }
    }
}