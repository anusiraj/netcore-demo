using NETCoreDemo.DTOs;
using NETCoreDemo.Models;

namespace NETCoreDemo.Services;

public class FakeCourseSerivce : FakeCrudService<Course, CourseDTO>, ICourseService
{
    public async Task<ICollection<Course>> GetCoursesByStatusAsync(Course.CourseStatus status)
    {
        return _items.Values
            .Where(c => c.Status == status)
            .ToList();
    }
}