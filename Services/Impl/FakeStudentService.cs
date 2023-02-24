using NETCoreDemo.DTOs;
using NETCoreDemo.Models;

namespace NETCoreDemo.Services;

public class FakeStudentService : FakeCrudService<Student, StudentDTO>, IStudentService
{
    public ICollection<Student> GetStudentsWithJobs()
    {
        throw new NotImplementedException();
    }

    public ICollection<Student> GetTopStudents()
    {
        throw new NotImplementedException();
    }
}