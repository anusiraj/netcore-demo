namespace NETCoreDemo.Controllers;

using NETCoreDemo.Services;
using NETCoreDemo.Models;
using NETCoreDemo.DTOs;
using Microsoft.AspNetCore.Mvc;

public abstract class CrudController<TModel, TDto> : ApiControllerBase
    where TModel : BaseModel
    where TDto : BaseDTO<TModel>
{
    private readonly ICrudService<TModel, TDto> _service;

    public CrudController(ICrudService<TModel, TDto> service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpPost]
    public virtual IActionResult Create(TDto request)
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
    public ActionResult<TModel?> Update(int id, TDto request)
    {
        var item = _service.Update(id, request);
        if (item is null)
        {
            return NotFound("Item is not found");
        }
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (_service.Delete(id))
        {
            return Ok(new { Message = "Item is deleted " });
        }
        return NotFound("Item is not found");
    }

    [HttpGet]
    public ICollection<TModel> GetAll()
    {
        return _service.GetAll();
    }
}