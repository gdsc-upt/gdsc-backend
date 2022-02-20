namespace GdscBackend.Utils;

public interface IEmailSender
{
    void SendEmail(string to, string subject, string body);
}