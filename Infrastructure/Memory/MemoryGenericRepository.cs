using AppCore.Dto;
using AppCore.Interfaces;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T> : IGenericRepositoryAsync<T>
    where T : class
{
    public Task<T?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<T?> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}