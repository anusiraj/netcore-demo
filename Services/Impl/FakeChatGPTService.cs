namespace NETCoreDemo.Services;

public class FakeChatGPTService : IChatGPTService
{
    private readonly ILogger<FakeChatGPTService> _logger;

    public FakeChatGPTService(ILogger<FakeChatGPTService> logger)
    {
        _logger = logger;
    }

    public string GetSugggestion(string message)
    {
        _logger.LogInformation("Faking ChatGPT message...");
        return "Fake chatGPT message";
    }
}