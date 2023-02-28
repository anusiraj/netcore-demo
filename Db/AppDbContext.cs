namespace NETCoreDemo.Db;

using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Models;
using Npgsql;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _config;
    //static constructor which run only once
    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Course.CourseStatus>();
        // You can also do that automatically using Reflection

        // Use the legacy timestamp behaviour - check Npgsql for more details
        // Recommendation from Postgres: Don't use time zone in database
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public AppDbContext(IConfiguration config) => _config = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = _config.GetConnectionString("MyDbConnection");
        optionsBuilder
            .UseNpgsql(connString)
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //map c# enum to postgres enum(1st step, 2ns step above)
        modelBuilder.HasPostgresEnum<Course.CourseStatus>();
        //create an index for Name using Fluent API, for easy searching with WHERE in queries
        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Name);

        base.OnModelCreating(modelBuilder); 
    }

    public DbSet<Course> Courses { get; set; }
}