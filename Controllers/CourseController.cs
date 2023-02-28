namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// TODO: Turn this into using a generic controller
public class CourseController : BaseController<Course, CourseDTO>
{
    private readonly ICourseService _service;

    public CourseController(ICourseService service) : base(service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    [HttpGet]
    public async Task<ICollection<Course>> GetCoursesByStatus([FromQuery] Course.CourseStatus status)
    {
        return await  _service.GetCoursesByStatusAsync(status);
    }   
}