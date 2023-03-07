using System.ComponentModel.DataAnnotations.Schema;

namespace NETCoreDemo.Models;

public class Project : BaseModel
{
    public string Name { get; set; } = null!;
    public string? Description {get; set; } =null!;

    [Column(TypeName = "jsonb")] //since it is collection of string db cannot recognize it so use annotation
    public ICollection<string> Tags { get; set; } = null!;
    public DateTime Deadline { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<ProjectStudent> StudentLinks { get; set; } //linking to join table(indirect many-many)
    //it wouldn't load by call request since it is a collection, we need to tell to load. So ovverride the getAsync method
}