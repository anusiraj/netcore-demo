namespace NETCoreDemo.Db;

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NETCoreDemo.Models;


//this class is for Interceptor ie. save some changes to db if there is eg, updatedeDate if data has updated
//one way is by triggering(like cascade on sql) db whenever it updated deleted or something happened -> good performance
//2nd way is the following from the entityframewrok itself
public class AppDbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    public void UpdateTimeStamps(DbContextEventData eventData)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries() //get back al the entries 
            .Where(e => e.Entity is BaseModel && (e.State == EntityState.Added) || (e.State == EntityState.Modified)); //filter above entries by entries form baseModel
            //e.State has diff queries like insert update delete, based on that this will proceed
        foreach(var entry in entries)
        {
            if(entry.State == EntityState.Added)
            {
                ((BaseModel)entry.Entity).CreatedAt = DateTime.Now;
            }
            else
            {
                ((BaseModel)entry.Entity).UpdatedAt = DateTime.Now;
            }
        }
    }
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateTimeStamps(eventData);
        return base.SavingChanges(eventData, result);
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateTimeStamps(eventData);
        return base.SavingChangesAsync(eventData, result);
    }


}