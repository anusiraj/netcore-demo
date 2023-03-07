namespace NETCoreDemo.Db;

using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Models;
using Npgsql;

public class AppDbContext : DbContext
{
    // Static constructor which will be run ONCE
    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Course.CourseStatus>(); //Warning bz use suitable for dontet7
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ProjectRole>();

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
            .AddInterceptors(new AppDbContextSaveChangesInterceptor())
            .UseSnakeCaseNamingConvention();
            // .UseLazyLoadingProxies(); // 3rd option(lazyloading) //first install
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map C# enum to Postgres enum
        modelBuilder.HasPostgresEnum<Course.CourseStatus>();//db create enum for us
        modelBuilder.HasPostgresEnum<ProjectRole>();


        // Create an index on Name using Fluent API
        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Name);

        //base.OnModelCreating(modelBuilder);

        //TODO: Change this repeating in a better way, make a loop
        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .Property(s => s.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Student>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Address>()
            .Property(s => s.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
         modelBuilder.Entity<Address>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Address) //relation with one property
            .WithOne() //empty bz address not need to track students so no student property in address model
            //but it 1-1 so need to mention .WithOne
            .OnDelete(DeleteBehavior.SetNull);

        //Tell EFCore to use the composite key otherwise it will not bz of not having primary key
        modelBuilder.Entity<ProjectStudent>()
            .HasKey(ps => new { ps.ProjectId, ps.StudentId});
        
        //always load the address along with student
        // modelBuilder.Entity<Student>()
        //     .Navigation(s => s.Address)
        //     .AutoInclude();
    }

    public DbSet<Course> Courses { get; set; } = null!; //Name can be whatever, just to declare
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!; 
    public DbSet<Project> Projects { get; set; } = null!; 
    public DbSet<ProjectStudent> ProjectStudents { get; set; } = null!; 

    
}
