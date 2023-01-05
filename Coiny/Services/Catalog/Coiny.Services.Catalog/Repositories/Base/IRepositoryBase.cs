using System.Linq.Expressions;
using Coiny.Common.Models.Base;

namespace Coiny.Services.Catalog.Repositories.Base;

public interface IRepositoryBase<T> where T: Entity
{
    IQueryable<T> Get(Expression<Func<T, bool>>? filter = null);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null);
    Task<T> GetByIdAsync(string id);
    Task<T> AddAsync(T entity);
    Task<bool> AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(string id, T entity);
    Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
    Task<T> DeleteAsync(T entity);
    Task<T> DeleteAsync(string id);
    Task<T> DeleteAsync(Expression<Func<T, bool>> filter);
}