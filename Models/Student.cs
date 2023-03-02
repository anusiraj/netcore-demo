using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NETCoreDemo.Models;

public class Student : BaseModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    [NotMapped] // to ignore from the db(so this is not clm inside the database)
    [JsonIgnore]
    public string FullName
    {
        get => $"{FirstName} {LastName}";
    }

    [MaxLength(256)]
    public string Email { get; set; } = null!;
    public virtual Address? Address {get; set; } //mark related entity as virtual for lazy loading purpose very important
    //bz it needs to overriden by proxies(for lazy loading)

    [JsonIgnore]
    public int? AddressId { get; set; } //should be optional. This is to tell this is the forign Id inorder for the case delete
}