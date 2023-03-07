using System;
using NETCoreDemo.Services;
using System.Text.Json.Serialization;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using NETCoreDemo.Db;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        //Fix the json cycle issue
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
var options = new JsonSerializerOptions
{
    ReadCommentHandling = JsonCommentHandling.Skip,
    AllowTrailingCommas = true,
};
builder.Services.AddDbContext<AppDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the services for dependency injection
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
// builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();
// builder.Services.AddScoped<IEmailSenderService, FakeEmailSenderService>();
// builder.Services.AddSingleton<IChatGPTService, FakeChatGPTService>();

// builder.Services.AddSingleton<ICounterService, RequestCounterService>();

// Change this to different lifetime and see how it works
builder.Services.AddScoped<IDemoService, DemoService>();

builder.Services.AddScoped<ICourseService, DbCourseSerivce>();

builder.Services.AddScoped<IStudentService, DbStudentService>();
builder.Services.AddScoped<ICrudService<Address, AddressDTO>, DbCrudService<Address, AddressDTO>>();

builder.Services.AddScoped<IAssignmentService, DbAssignmentService>();
builder.Services.AddScoped<IProjectService, DbProjectService>();


builder.Services.Configure<CourseSetting>(builder.Configuration.GetSection("MyCourseSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();

    // NOTE: Don't do this in production
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        if (dbContext is not null)
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
