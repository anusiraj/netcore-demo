namespace NETCoreDemo.Models;

public class Assignment : BaseModel
{
    public DateTime Deadline { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}