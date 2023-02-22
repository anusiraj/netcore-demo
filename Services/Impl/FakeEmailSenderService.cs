namespace NETCoreDemo.Services;

public class FakeEmailSenderService : IEmailSenderService
{
    private readonly ILogger<FakeEmailSenderService> _logger;

    public FakeEmailSenderService(ILogger<FakeEmailSenderService> logger)
    {
        _logger = logger;
    }

    public bool SendEmail(string to, string subject, string? body = null)
    {
        _logger.LogInformation($"Sending the email to {to}, subject: {subject}, body: {body}");
        return true;
    }
}