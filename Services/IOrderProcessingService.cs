namespace NETCoreDemo.Services;

using NETCoreDemo.Models;

public interface IOrderProcessingService
{
    bool ProcessOrder(Order order);
}