using GdscBackend.Features.Contacts;

namespace GdscBackend.Utils.Services;

public interface IWebhookService
{
    void SendContact(ContactModel contact);
}