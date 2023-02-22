namespace NETCoreDemo.Services;

public class DemoService : IDemoService
{
    private readonly ILogger<DemoService> _logger;

    public DemoService(ILogger<DemoService> logger)
    {
        _logger = logger;
        _logger.LogInformation("In demo service constructor");
    }

    public void Demo()
    {
        _logger.LogInformation("Demoing...");
    }
}