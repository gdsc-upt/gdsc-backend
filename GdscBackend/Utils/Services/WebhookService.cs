namespace GdscBackend.Utils.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly string _url;

        public WebhookService(string url)
        {
            _url = url;
        }

        public async void SendMessage(string author, string mail, string subject, string message)
        {
            var webhook = new Webhook(_url);
            await webhook.Send(author, mail, subject, message);
        }
    }
}