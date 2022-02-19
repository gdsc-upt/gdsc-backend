using GdscBackend.Models;
using GdscBackend.Utils.Services;
using Xunit.Abstractions;

namespace GdscBackend.Tests.Mocks;

public class TestWebhookService : IWebhookService
{
    private readonly ITestOutputHelper _outputHelper;

    public TestWebhookService(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    public void SendContact(ContactModel contact)
    {
        _outputHelper.WriteLine("Message sent by: " + contact.Name);
        _outputHelper.WriteLine("With email: " + contact.Email);
        _outputHelper.WriteLine("Message subject: " + contact.Subject);
        _outputHelper.WriteLine("Message content: " + contact.Text);
    }
}