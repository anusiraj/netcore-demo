namespace NETCoreDemo.Db;

using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Models;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;

    public AppDbContext(IConfiguration config) => _config = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = _config.GetConnectionString("DefaultConnection");
        optionsBuilder
            .UseNpgsql(connString)
            .UseSnakeCaseNamingConvention();
    }

    public DbSet<Course> Courses { get; set; }
}
