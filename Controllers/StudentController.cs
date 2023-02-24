namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

public class StudentController : ApiControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly ICrudService<Student, StudentDTO> _service;

    public StudentController(ILogger<StudentController> logger, ICrudService<Student, StudentDTO> service)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost]
    public IActionResult Create(StudentDTO request)
    {
        var student = _service.Create(request);
        if (student is null)
        {
            return BadRequest();
        }
        return Ok(student);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Student?> Get(int id)
    {
        var student = _service.Get(id);
        if (student is null)
        {
            return NotFound("Student is not found");
        }
        return student;
    }

    [HttpPut("{id:int}")]
    public ActionResult<Student?> Update(int id, StudentDTO request)
    {
        var student = _service.Update(id, request);
        if (student is null)
        {
            return NotFound("Student is not found");
        }
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (_service.Delete(id))
        {
            return Ok(new { Message = "Student is deleted " });
        }
        return NotFound("Student is not found");
    }

    [HttpGet]
    public ICollection<Student> GetAll()
    {
        return _service.GetAll();
    }
}