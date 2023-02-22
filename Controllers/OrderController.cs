namespace NETCoreDemo.Controllers;

using Microsoft.AspNetCore.Mvc;
using NETCoreDemo.Models;
using NETCoreDemo.Services;

public class OrderController : ApiControllerBase
{
    private readonly IOrderProcessingService _service;
    private readonly ILogger<OrderController> _logger;
    private readonly IDemoService _demo;
    private readonly ICounterService _counterService;

    public OrderController(IOrderProcessingService service,
                            ILogger<OrderController> logger,
                            IDemoService demo,
                            ICounterService counterService
    )
    {
        _service = service;
        _logger = logger;
        _demo = demo;
        _counterService = counterService;
        _counterService.Increase();
    }

    [HttpGet]
    public IActionResult DoDemo([FromServices] IDemoService demo)
    {
        demo.Demo();
        return NoContent();
    }

    [HttpPost]
    public IActionResult MakeOrder(Order order)
    {
        if (!_service.ProcessOrder(order))
        {
            _logger.LogError($"Failed to process order for customer: {order.CustomerId}");
        }
        return Ok(new { Message = "Order has been processed successfully" });
    }
}