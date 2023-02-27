namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

public class CourseController : CrudController<Course, CourseDTO>
{
    private readonly ICourseService _service;

    public CourseController(ICourseService service) : base(service)
    {
        _service = service;
    }

    [HttpGet]
    public ICollection<Course> GetCoursesByStatus([FromQuery] Course.CourseStatus status)
    {
        return _service.GetCoursesByStatus(status);
    }
}