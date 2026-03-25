using AppCore.Dto;
using AppCore.Interfaces;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T>: IGenericRepositoryAsync<T> where T: class 
{
    protected Dictionary<Guid, T> _data = new();
    
    public Task<T?> GetByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

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
        var id = Guid.NewGuid();
        _data[id] = entity;
        return Task.FromResult(_data[id]);
    }

    public Task<T> UpdateAsync(T entity)
    {
        /* TODO: What UpdateAsync have to do */
        throw new NotImplementedException();
    }

    public Task RemoveByIdAsync(Guid id)
    {
        var result = _data.Remove(id);
        return Task.FromResult(result);
    }
}