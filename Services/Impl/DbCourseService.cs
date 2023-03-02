using NETCoreDemo.Db;
using NETCoreDemo.DTOs;
using NETCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NETCoreDemo.Pagination;

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
    public async Task<ICollection<Course>> GetAllAsync([FromQuery] PaginationFilter filter)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await _dbContext.Courses
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();
        var totalRecords = await _dbContext.Courses.CountAsync();
        return Ok(new PagedResponse<List<Course>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
    }

  private ICollection<Course> Ok(PagedResponse<List<Course>> pagedResponse)
  {
    throw new NotImplementedException();
  }
}