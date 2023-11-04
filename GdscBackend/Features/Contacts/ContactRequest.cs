using GdscBackend.Common.RequestModels;

namespace GdscBackend.Features.Contacts;

public class ContactRequest : Request
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Subject { get; set; }

    public string Text { get; set; }
}