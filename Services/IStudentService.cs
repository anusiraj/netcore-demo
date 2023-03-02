namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public interface IStudentService : ICrudService<Student, StudentDTO>
{
    ICollection<Student> GetTopStudents();
    ICollection<Student> GetStudentsWithJobs();
}