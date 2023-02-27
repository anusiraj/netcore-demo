namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public class StudentController : CrudController<Student, StudentDTO>
{
    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger, ICrudService<Student, StudentDTO> service) : base(service)
    {
        _logger = logger;
    }
}