namespace NETCoreDemo.Services;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Forecast(int days);
}
