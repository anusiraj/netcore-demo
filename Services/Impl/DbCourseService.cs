using NETCoreDemo.Db;
using NETCoreDemo.DTOs;
using NETCoreDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace NETCoreDemo.Services;

public class DbCourseSerivce : DbCrudService<Course, CourseDTO>, ICourseService
{
    public DbCourseSerivce(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Course>> GetCoursesByStatusAsync(Course.CourseStatus status)
    {
        return await _dbContext.Courses
            .Where(c => c.Status == status)
            .ToListAsync();
    }
}