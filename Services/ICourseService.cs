namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public interface ICourseService
{
    // CRUD operations
    // C - Create
    // R - Read (Get)
    // U - Update
    // D - Delete
    Course? Create(CourseDTO request);
    Course? Get(int id);
    Course? Update(int id, CourseDTO request);
    bool Delete(int id);
    ICollection<Course> GetAll();
}