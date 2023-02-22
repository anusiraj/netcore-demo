namespace NETCoreDemo.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IChatGPTService _chatGPTService;

    public EmailSenderService(IChatGPTService chatGPTService)
    {
        _chatGPTService = chatGPTService;
    }

    public bool SendEmail(string to, string subject, string? body = null)
    {
        var emailBody = body;

        if (body is null)
        {
            emailBody = _chatGPTService.GetSugggestion(subject);
        }
        // Send the real email with subject and body
        return true;
    }
}