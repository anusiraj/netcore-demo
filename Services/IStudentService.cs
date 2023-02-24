namespace NETCoreDemo.Services;

using NETCoreDemo.Models;

public interface IStudentService
{
    ICollection<Student> GetTopStudents();
    ICollection<Student> GetStudentsWithJobs();
}