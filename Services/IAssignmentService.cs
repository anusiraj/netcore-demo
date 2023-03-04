namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public interface IAssignmentService : ICrudService<Assignment,AssignmentDTO>
{
    Task<int> AssignStudentsAsync(int id, ICollection<int> students);

    //TODO REMOVE
    Task<int> RemoveStudentsAsync(int id, ICollection<int> students);
}