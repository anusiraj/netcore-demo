namespace NETCoreDemo.Controllers;

using Microsoft.AspNetCore.Mvc;
using NETCoreDemo.Services;

public class CounterController : ApiControllerBase
{
    private readonly ILogger<CounterController> _logger;
    private readonly ICounterService _counterService;

    public CounterController(ILogger<CounterController> logger, ICounterService counterService)
    {
        _logger = logger;
        _counterService = counterService;
    }

    [HttpGet]
    public IActionResult GetCounter()
    {
        return Ok(new { CounterValue = _counterService.CurrentValue });
    }
}