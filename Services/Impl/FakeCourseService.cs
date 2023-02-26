using NETCoreDemo.DTOs;
using NETCoreDemo.Models;
using System.Collections.Concurrent;

namespace NETCoreDemo.Services;

public class FakeCourseSerivce : ICourseService
{
    private ConcurrentDictionary<int, Course> _courses = new(); //thread safe usage. there will be 1 single thread, other thread will wait
    //Dictionary has 1 property to map the object(course) with id rather than list doesnt have
    private int _courseId;

    public Course? Create(CourseDTO request)
    {
        var course = new Course
        {
            Id = Interlocked.Increment(ref _courseId), // Atomic operation(for thread safety, wait for the first operation to be done
            Name = request.Name,
            StartDate = request.StartDate,
            Status = request.Status,
            Size = request.Size,
        };
        _courses[course.Id] = course; //put that id into the dictionary to map it from the id to the course obj
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
        return _courses.Values;//Values are keys and values
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