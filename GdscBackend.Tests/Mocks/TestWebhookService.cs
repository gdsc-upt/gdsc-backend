using GdscBackend.Utils.Services;
using Xunit.Abstractions;

namespace GdscBackend.Tests.Mocks
{
    public class TestWebhookService : IWebhookService
    {
        private readonly ITestOutputHelper _outputHelper;
        public TestWebhookService(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public void SendContact(string author, string mail, string subject, string message)
        {
            _outputHelper.WriteLine("Message sent by: " + author);
            _outputHelper.WriteLine("With email: " + mail);
            _outputHelper.WriteLine("Message subject: " + subject);
            _outputHelper.WriteLine("Message content: " + message);
        }
    }
}