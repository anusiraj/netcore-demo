namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

public class ProjectController : CrudController<Project, ProjectDTO>
{
    private readonly ILogger<ProjectController> _logger;
    private readonly IProjectService _projectservice;

    public ProjectController(ILogger<ProjectController> logger, IProjectService service) : base(service)
    {
        _logger = logger;
        _projectservice = service;
    }

    [HttpPost("{id}/add-students")]
    public async Task<IActionResult> AddStudents(int id, ICollection<ProjctAddStudentsDTO> request)
    {
        var added = await _projectservice.AddStudentsAsync(id, request);
        if(added <= 0)
        {
            return BadRequest("No valid student found");
        }
        return Ok( new{ Count = added });
    }
}