namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

// TODO: Turn this into a generic controller - done
public class StudentController : BaseController<Student, StudentDTO>
{
  private readonly ILogger<StudentController> _logger;
  public StudentController(ILogger<StudentController> logger, ICrudService<Student, StudentDTO> service) : base(service)
  {
      _logger = logger;
  }
}