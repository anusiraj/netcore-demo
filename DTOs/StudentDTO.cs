using System.ComponentModel.DataAnnotations;
namespace NETCoreDemo.DTOs;

using NETCoreDemo.Models;

// FIXME: Code doesn't compile - done
public class StudentDTO : BaseDTO<Student>
{
    [MinLength(3)]
    public string FirstName { get; set; } = string.Empty;

    [MinLength(3)]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

  public override void UpdateModel(Student model)
  {
    model.FirstName = FirstName;
    model.LastName = LastName;
    model.Email = Email;
    model.UpdatedAt = DateTime.Now;
  }
}