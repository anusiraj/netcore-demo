namespace NETCoreDemo.Models;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    // TODO: Update this field whenever the model is updated
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}