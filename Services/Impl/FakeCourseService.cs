using NETCoreDemo.DTOs;
using NETCoreDemo.Models;
using System.Collections.Concurrent;

namespace NETCoreDemo.Services;

public class FakeCourseSerivce : ICourseService
{
    private ConcurrentDictionary<int, Course> _courses = new();
    private int _courseId;

    public Course? Create(CourseDTO request)
    {
        var course = new Course
        {
            Id = Interlocked.Increment(ref _courseId), // Atomic operation
            Name = request.Name,
            StartDate = request.StartDate,
            Status = request.Status,
            Size = request.Size,
        };
        _courses[course.Id] = course;
        return course;
    }

    public bool Delete(int id)
    {
        if (!_courses.ContainsKey(id))
        {
            return false;
        }
        return _courses.Remove(id, out var _);
    }

    public Course? Get(int id)
    {
        if (_courses.TryGetValue(id, out var course))
        {
            return course;
        }
        return null;
    }

    public ICollection<Course> GetAll()
    {
        return _courses.Values;
    }

    public ICollection<Course> GetCoursesByStatus(Course.CourseStatus status)
    {
        return _courses.Values
            .Where(c => c.Status == status)
            .ToList();
    }

    public Course? Update(int id, CourseDTO request)
    {
        var course = Get(id);
        if (course is null)
        {
            return null;
        }
        course.Name = request.Name;
        course.Status = request.Status;
        course.StartDate = request.StartDate;
        return course;
    }
}