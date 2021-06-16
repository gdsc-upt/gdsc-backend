namespace GdscBackend.Utils.Services
{
    public interface IWebhookService
    {
        void SendMessage(string author, string mail, string subject, string message);
    }
}