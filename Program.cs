using NETCoreDemo.Services;
using System.Text.Json.Serialization;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using NETCoreDemo.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// FIXME: convert enum string to number and viceversa - done
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDbContext<AppDbContext>(); //tell the framework about AppDbContext
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
builder.Services.AddScoped<ICourseService, DbCourseSerivce>();

// FIXME: Missing service registration - done
builder.Services.AddSingleton<ICrudService<Student,StudentDTO>, FakeCrudService<Student, StudentDTO>>();
// FIXME: Missing configuration registration for IOptions<CourseSetting> - done
builder.Services.Configure<CourseSetting>(builder.Configuration.GetSection("MyCourseSettings")); // to DI the configured coursesetting from appsettings

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();

    //do not do this in production environment
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        if(dbContext is not null)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
