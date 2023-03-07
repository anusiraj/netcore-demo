namespace NETCoreDemo.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Db;
using NETCoreDemo.DTOs;
using NETCoreDemo.Models;

public class DbStudentService : DbCrudService<Student, StudentDTO>, IStudentService
{
    public DbStudentService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<ICollection<Student>> GetAllAsync()
    {
        return await _dbContext.Students 
            // load the address along with student(2nd option, 1st and 2nd(Lazy loading) on context file)
            //EagerLoading
            .AsNoTracking()
            .Include(s => s.Address)
            .Include(s => s.Course)
            .Include(s => s.Assignments)
            .Include(s => s.ProjectLinks) // => a list of ProjectStudent
                .ThenInclude(ps => ps.Project) // => returns project from that list
                // .ThenInclude(ps => new{ps.Project, ps.Student}) //if we need student also
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public override async Task<Student?> GetAsync(int id)
    {
        // return await _dbContext.Students
        //     //Lazy loading remaining things in between mark related attribute as virtual
        //     //use this when you dont know when oyu need to display
        //     .Include(s => s.Address)
        //     .FirstOrDefaultAsync(s => s.Id == id);

        //Individual Loading or Explicit Loading(4th option) use this when it is object specific like if student.FirstName == "Anu"
        var student = await base.GetAsync(id);
            if(student is null)
            {
                return null;
            }
            await _dbContext.Entry(student).Reference(s => s.Address).LoadAsync(); //go and load the address
            return student;
    }

    public ICollection<Student> GetStudentsWithJobs()
    {
        throw new NotImplementedException();
    }

    public ICollection<Student> GetTopStudents()
    {
        throw new NotImplementedException();
    }
}
