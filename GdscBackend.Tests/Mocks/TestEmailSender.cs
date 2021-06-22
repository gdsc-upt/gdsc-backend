using GdscBackend.Utils;
using Xunit.Abstractions;

namespace GdscBackend.Tests.Mocks
{
    public class TestEmailSender : IEmailSender
    {
        private readonly ITestOutputHelper _outputHelper;

        public TestEmailSender(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        public void SendEmail(string to, string subject, string body)
        {
            _outputHelper.WriteLine("Email sent to: " + to);
            _outputHelper.WriteLine("Email sent with subject: " + subject);
            _outputHelper.WriteLine("Email sent with body: " + body);
        }
    }
}