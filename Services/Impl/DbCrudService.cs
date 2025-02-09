namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using NETCoreDemo.Db;
using Microsoft.EntityFrameworkCore;

public class DbCrudService<TModel, TDto> : ICrudService<TModel, TDto>
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    protected readonly AppDbContext _dbContext;

    public DbCrudService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TModel?> CreateAsync(TDto request)
    {
        var item = new TModel();
        request.UpdateModel(item);
        _dbContext.Add(item);
        await _dbContext.SaveChangesAsync(); // Tell the db context to update the database
        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await GetAsync(id);
        if (item is null)
        {
            return false;
        }
        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public virtual async Task<ICollection<TModel>> GetAllAsync()
    {
        return await _dbContext.Set<TModel>().AsNoTracking().ToListAsync(); //no tracking by cache bz it is just returning all data straight from the db
    }

    public virtual async Task<TModel?> GetAsync(int id)
    {
        return await _dbContext.Set<TModel>().FindAsync(id);
    }

    public async Task<TModel?> UpdateAsync(int id, TDto request)
    {
        var item = await GetAsync(id);
        if (item is null)
        {
            return null;
        }
        request.UpdateModel(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
}