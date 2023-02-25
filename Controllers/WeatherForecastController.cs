using Microsoft.AspNetCore.Mvc;
using NETCoreDemo.Services;
using NETCoreDemo.Models;

namespace NETCoreDemo.Controllers;

public class WeatherForecastController : ApiControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherForecastService _service;
    private readonly ICounterService _counterService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, 
                                        IWeatherForecastService service,
                                        ICounterService counterService)
    {
        _logger = logger;
        _service = service;
        _logger.LogInformation("This is the constructor");
        _counterService = counterService;
        _counterService.Increase();
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
