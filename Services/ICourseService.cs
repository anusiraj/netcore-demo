namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public interface ICourseService : ICrudService<Course, CourseDTO>
{
    Task<ICollection<Course>> GetCoursesByStatusAsync(Course.CourseStatus status);
    Task<ICollection<Course>> GetAllAsync();
}