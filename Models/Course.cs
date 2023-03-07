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

    public ICollection<Student> Students { get; set; } = null!; 

    //no need of related entities id(StudentId) in principal side

    [NotMapped]
    public ICollection<string> Images { get; set; } = null!;

    public enum CourseStatus
    {
        NotStarted,
        OnGoing,
        Ended,
    }
}