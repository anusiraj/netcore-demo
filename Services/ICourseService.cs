namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public interface ICourseService : ICrudService<Course, CourseDTO>
{
    ICollection<Course> GetCoursesByStatus(Course.CourseStatus status);
}