namespace NETCoreDemo.Models;

public class Order
{
    public int CustomerId { get; set; }
    public ICollection<OrderItem>? Items { get; set; }
}

public class OrderItem
{
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public double ProductPrice { get; set; }
}