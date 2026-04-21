using AppCore.Dto;
using AppCore.Models;

namespace AppCore.Interfaces;

public interface IGenericRepositoryAsync<T> where T : EntityBase
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<PagedResult<T>> FindPagedAsync(int page, int pageSize);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task RemoveByIdAsync(Guid id);
}