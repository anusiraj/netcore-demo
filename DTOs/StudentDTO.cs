using System.ComponentModel.DataAnnotations;
namespace NETCoreDemo.DTOs;

using NETCoreDemo.Models;

public class StudentDTO : BaseDTO<Student>
{
    [MinLength(3)]
    public string FirstName { get; set; } = string.Empty;

    [MinLength(3)]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public AddressDTO Address { get; set; } = null!;

    public override void UpdateModel(Student model)
    {
        model.FirstName = FirstName;
        model.LastName = LastName;
        model.Email = Email;
        var address = new Address(); //save the payload(updated address) to this obj
        Address.UpdateModel(address);//update the new address in Address table
        model.Address = address;// update hat address also in the student table.
    }
}