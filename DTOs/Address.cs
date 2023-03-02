namespace NETCoreDemo.DTOs;

using NETCoreDemo.Models;

public class AddressDTO : BaseDTO<Address>
{
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    //address not need to track the students details so no need to mention related entities props

  public override void UpdateModel(Address model)
  {
    model.Street = Street;
    model.City = City;
    model.Zipcode = Zipcode;
  }
}