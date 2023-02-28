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

    // TODO: Combine this with the GetAll() method from the base class
    // 1. If no status is given on query string, return all
    // 2. Otherwise, filter the courses by status
    [HttpGet("by-status")]
    public async Task<ICollection<Course>> GetCoursesByStatus([FromQuery] Course.CourseStatus status)
    {
        return await _service.GetCoursesByStatusAsync(status);
    }
}