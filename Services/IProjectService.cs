namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public interface IProjectService : ICrudService<Project, ProjectDTO>
{
    Task<int> AddStudentsAsync(int id, ICollection<ProjctAddStudentsDTO> students);
    //TODO: Implement a method to remove students and add an endpoint for it
}