using AppCore.Dto;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T>: IGenericRepositoryAsync<T> where T: EntityBase 
{
    protected Dictionary<Guid, T> _data = new();
    
    public Task<T?> GetByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

    public Task<T?> FindByIdAsync(Guid id) => GetByIdAsync(id);

    public Task<IEnumerable<T>> FindAllAsync()
    {
        IEnumerable<T> result = _data.Values.ToList();
        return Task.FromResult(result);
    }

    public Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
    {
        var result = _data.Values.ToList();
        return Task.FromResult(new PagedResult<T>(result, result.Count, page, pageSize));
    }

    public Task<T> AddAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        _data[entity.Id] = entity;
        return Task.FromResult(_data[entity.Id]);
    }

    public Task<T> UpdateAsync(T entity)
    {
        if (_data.ContainsKey(entity.Id))
        {
            _data[entity.Id] = entity;
            return Task.FromResult(_data[entity.Id]);
        }
        throw new KeyNotFoundException();
    }

    public Task RemoveByIdAsync(Guid id)
    {
        var result = _data.Remove(id);
        return Task.FromResult(result);
    }
}