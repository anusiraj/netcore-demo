namespace NETCoreDemo.Models;

public class Course : BaseModel
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public CourseStatus Status { get; set; }
    public int Size { get; set; }

    public enum CourseStatus
    {
        NotStarted,
        OnGoing,
        Ended,
    }
}