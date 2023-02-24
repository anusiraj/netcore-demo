using NETCoreDemo.DTOs;
using NETCoreDemo.Models;

namespace NETCoreDemo.Services;

public class FakeCrudService<TModel, TDto> : ICrudService<TModel, TDto>
    where TModel
    where TDto : BaseDTO<TModel>
{
    // FIXME: This is not thread-safe
    private Dictionary<int, TModel> _items = new();
    private int _itemId;

    public TModel? Create(TDto request)
    {
        // FIXME: Code doesn't compile
        var item = new TModel
        {
            Id = Interlocked.Increment(ref _itemId), // Atomic operation
        };
        _items[item.Id] = item;
        // TODO: Updating item from dto
        return item;
    }

    public bool Delete(int id)
    {
        if (!_items.ContainsKey(id))
        {
            return false;
        }
        return _items.Remove(id, out var _);
    }

    public TModel? Get(int id)
    {
        if (_items.TryGetValue(id, out var item))
        {
            return item;
        }
        return null;
    }

    public ICollection<TModel> GetAll()
    {
        return _items.Values;
    }

    public TModel? Update(int id, TDto request)
    {
        var item = Get(id);
        if (item is null)
        {
            return null;
        }
        // TODO: Updating item from dto
        return item;
    }
}