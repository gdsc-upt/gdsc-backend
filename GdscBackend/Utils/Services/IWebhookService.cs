namespace GdscBackend.Utils.Services
{
    public interface IWebhookService
    {
        void SendContact(string author, string mail, string subject, string message);
    }
}