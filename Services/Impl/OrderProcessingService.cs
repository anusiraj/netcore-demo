using NETCoreDemo.Models;

namespace NETCoreDemo.Services;

public class OrderProcessingService : IOrderProcessingService
{
    private readonly IEmailSenderService _emailSender;

    public OrderProcessingService(IEmailSenderService emailSender)
    {
        _emailSender = emailSender;
    }

    private string GetCustomerEmail(int customerId) => $"customer{customerId}@gmail.com";

    public bool ProcessOrder(Order order)
    {
        var customerEmail = GetCustomerEmail(order.CustomerId);
        var subject = HandleOrder(order) switch
        {
            true => "Your order has been processed successfully",
            _ => "Your order failed to be processd"
        };
        return _emailSender.SendEmail(customerEmail, subject, null);
    }

    private bool HandleOrder(Order order)
    {
        // Some logics to handle the order here
        return true;
    }
}