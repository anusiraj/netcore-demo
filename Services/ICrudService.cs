namespace NETCoreDemo.Services;

using NETCoreDemo.Models;
using NETCoreDemo.DTOs;

public interface ICrudService<TModel, TDto>
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    TModel? Create(TDto request);
    TModel? Get(int id);
    TModel? Update(int id, TDto request);
    bool Delete(int id);
    ICollection<TModel> GetAll();
}