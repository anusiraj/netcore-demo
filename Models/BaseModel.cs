namespace NETCoreDemo.Models;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}