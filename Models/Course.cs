using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NETCoreDemo.Models;

public class Course : BaseModel
{
    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public CourseStatus Status { get; set; }

    // Using data annotation (attribute)
    [Column(TypeName = "smallint")]
    public int Size { get; set; }

    public enum CourseStatus
    {
        NotStarted,
        OnGoing,
        Ended,
    }
}