namespace NETCoreDemo.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public CourseStatus Status { get; set; }

    public enum CourseStatus
    {
        NotStarted,
        OnGoing,
        Ended,
    }
}