namespace NETCoreDemo.Db;

using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Models;
using Npgsql;

public class AppDbContext : DbContext
{
    // Static constructor which will be run ONCE
    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Course.CourseStatus>();
        // You can also do that automatically using Reflection

        // Use the legacy timestamp behaviour - check Npgsql for more details
        // Recommendation from Postgres: Don't use time zone in database
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    private readonly IConfiguration _config;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options) => _config = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connString = _config.GetConnectionString("DefaultConnection");
        optionsBuilder
            .UseNpgsql(connString)
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map C# enum to Postgres enum
        modelBuilder.HasPostgresEnum<Course.CourseStatus>();

        // Create an index on Name using Fluent API
        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Name);

        //base.OnModelCreating(modelBuilder);
    }

    public DbSet<Course> Courses { get; set; } = null!;
}
