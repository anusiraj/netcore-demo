using System.Collections.Concurrent;
using NETCoreDemo.DTOs;
using NETCoreDemo.Models;

namespace NETCoreDemo.Services;

public class FakeCrudService<TModel, TDto> : ICrudService<TModel, TDto>
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    protected ConcurrentDictionary<int, TModel> _items = new();
    private int _itemId;

    public async Task<TModel?> CreateAsync(TDto request)
    {
        var item = new TModel
        {
            Id = Interlocked.Increment(ref _itemId), // Atomic operation
        };
        _items[item.Id] = item;
        request.UpdateModel(item);
        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!_items.ContainsKey(id))
        {
            return false;
        }
        return _items.Remove(id, out var _);
    }

    public async Task<TModel?> GetAsync(int id)
    {
        if (_items.TryGetValue(id, out var item))
        {
            return item;
        }
        return null;
    }

    public async Task<ICollection<TModel>> GetAllAsync()
    {
        return _items.Values;
    }

    public async Task<TModel?> UpdateAsync(int id, TDto request)
    {
        var item = await GetAsync(id);
        if (item is null)
        {
            return null;
        }
        request.UpdateModel(item);
        return item;
    }
}