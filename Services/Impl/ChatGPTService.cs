namespace NETCoreDemo.Services;

public class ChatGPTService : IChatGPTService
{
    private readonly ILogger<ChatGPTService> _logger;

    public ChatGPTService(ILogger<ChatGPTService> logger)
    {
        _logger = logger;
    }

    public string GetSugggestion(string message)
    {
        // Connect to chat GPT and ask for a message
        _logger.LogInformation("Connecting to real ChatGPT for message...");
        return message;
    }
}