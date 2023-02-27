using Microsoft.AspNetCore.Mvc;
using NETCoreDemo.DTOs;
using NETCoreDemo.Models;
using NETCoreDemo.Services;

namespace NETCoreDemo.Controllers;

public abstract class BaseController<TModel, TDto> : ApiControllerBase
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    // private readonly ILogger<TController> _logger;
    private readonly ICrudService<TModel, TDto> _service;

    public BaseController(ICrudService<TModel, TDto> service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost]
    public virtual IActionResult Update(TDto request)
    {
        var item = _service.Create(request);
        if (item is null)
        {
            return BadRequest();
        }
        return Ok(item);
    }

    [HttpGet("{id:int}")]
    public virtual ActionResult<TModel?> Get(int id)
    {
        var item = _service.Get(id);
        if (item is null)
        {
            return NotFound("Item is not found");
        }
        return item;
    }

    [HttpPut("{id:int}")]
    public virtual ActionResult<TModel?> Update(int id, TDto request)
    {
        var item = _service.Update(id, request);
        if (item is null)
        {
            return NotFound($"{item} is not found");
        }
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public virtual ActionResult Delete(int id)
    {
        if (_service.Delete(id))
        {
            return Ok(new { Message = "Item is deleted " });
        }
        return NotFound("Item is not found");
    }

    [HttpGet]
    public virtual ICollection<TModel> GetAll()
    {
        return _service.GetAll();
    }
}