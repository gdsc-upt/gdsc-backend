using GdscBackend.Models;

namespace GdscBackend.Utils.Services
{
    public interface IWebhookService
    {
        void SendContact(ContactModel contact);
    }
}