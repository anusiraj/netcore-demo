using NETCoreDemo.Services;
using System.Text.Json.Serialization;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// FIXME: convert enum string to number and viceversa - done
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the services for dependency injection
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();
builder.Services.AddScoped<IEmailSenderService, FakeEmailSenderService>();
builder.Services.AddSingleton<IChatGPTService, FakeChatGPTService>();

builder.Services.AddSingleton<ICounterService, RequestCounterService>();

// Change this to different lifetime and see how it works
builder.Services.AddTransient<IDemoService, DemoService>();
builder.Services.AddSingleton<ICourseService, FakeCourseSerivce>();

// FIXME: Missing service registration - done
builder.Services.AddSingleton<ICrudService<Student,StudentDTO>, FakeCrudService<Student, StudentDTO>>();
// FIXME: Missing configuration registration for IOptions<CourseSetting> - done
builder.Services.Configure<CourseSetting>(builder.Configuration.GetSection("MyCourseSettings")); // to DI the configured coursesetting from appsettings

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
