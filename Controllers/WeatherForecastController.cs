using Microsoft.AspNetCore.Mvc;
using NETCoreDemo.Services;

namespace NETCoreDemo.Controllers;

[ApiController]
[Route("[controller]s")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _service;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
    {
        _logger = logger;
        _service = service;
        _logger.LogInformation("This is the constructor");
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _service.Forecast(3);
    }

    [HttpGet("{numDays}")]
    public IEnumerable<WeatherForecast> GetByDays([FromRoute(Name = "numDays")] int days)
    {
        return _service.Forecast(days);
    }
}
