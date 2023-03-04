namespace NETCoreDemo.Services;

using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Db;
using NETCoreDemo.DTOs;
using NETCoreDemo.Models;

public class DbAssignmentService : DbCrudService<Assignment, AssignmentDTO>, IAssignmentService
{
  public DbAssignmentService(AppDbContext dbContext) : base(dbContext)
  {
  }

  public async Task<int> AssignStudentsAsync(int id, ICollection<int> studentIds)
  {
    var assignment = await GetAsync(id);
    {
        if(assignment is null)
        {
            return -1;
        }
        var students = await _dbContext.Students
            .Include(s => s.Assignments) //Eager Loading
            .Where(s => studentIds.Contains(s.Id))
            .ToListAsync();
        
        foreach(var student in students)
        {
            //dont include anything inside loop
            //when doing excplicit loading here since it is loop create sizeof9
            if(!student.Assignments.Contains(assignment))
            {
                student.Assignments.Add(assignment);
            }
        }
        await _dbContext.SaveChangesAsync();
        return students.Count();
            
    }
  }
 //TODO REmove
  public Task<int> RemoveStudentsAsync(int id, ICollection<int> students)
  {
    throw new NotImplementedException();
  }
}