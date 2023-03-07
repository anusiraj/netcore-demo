namespace NETCoreDemo.Models;

using System.ComponentModel.DataAnnotations;

public class ProjectStudent //no need to extend from baseModel
{
    public Student Student { get; set; }
    public Project Project { get; set; }
    public DateTime JoinedAt { get; set; }
    public ProjectRole Role {get; set; }

    // [Key] //to indicate it is the composite key
    public int StudentId {get; set; }

    // [Key] got with fluentAPI
    public int ProjectId {get; set; }


}