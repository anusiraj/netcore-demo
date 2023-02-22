namespace NETCoreDemo.Services;

public interface IEmailSenderService
{
    bool SendEmail(string to, string subject, string? body);
}