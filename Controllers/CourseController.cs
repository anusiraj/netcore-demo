namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// TODO: Turn this into using a generic controller
public class CourseController : ApiControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly ICourseService _service;
    private readonly IConfiguration _config;
    private readonly IOptions<CourseSetting> _settings;

    public CourseController(ILogger<CourseController> logger, ICourseService service, IConfiguration config, IOptions<CourseSetting> settings)
    {
        _logger = logger;
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _config = config;
        _settings = settings;
    }

    [HttpPost]
    public IActionResult Create(CourseDTO request)
    {
        if (request.Size < _settings.Value.MinSize || request.Size > _settings.Value.MaxSize)
        {
            return BadRequest(new { Message = "Course size is not valid" });
        }

        var course = _service.Create(request);
        if (course is null)
        {
            return BadRequest();
        }
        return Ok(course);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Course?> Get(int id)
    {
        var course = _service.Get(id);
        if (course is null)
        {
            return NotFound("Course is not found");
        }
        return course;
    }

    [HttpPut("{id:int}")]
    public ActionResult<Course?> Update(int id, CourseDTO request)
    {
        var course = _service.Update(id, request);
        if (course is null)
        {
            return NotFound("Course is not found");
        }
        return Ok(course);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (_service.Delete(id))
        {
            return Ok(new { Message = "Course is deleted " });
        }
        return NotFound("Course is not found");
    }

    [HttpGet]
    public ICollection<Course> GetAll()
    {
        return _service.GetAll();
    }
}