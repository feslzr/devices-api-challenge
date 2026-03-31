using Challenge.Domain.Models;
using System.Linq.Expressions;

namespace Challenge.Application.Interfaces.Repository;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllListAsync();
    IQueryable<TEntity> GetAllAsync();
    IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression);
    Pagination<TEntity> FindAllPaginate(Expression<Func<TEntity, bool>> expression, int offset, int limit = 100);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    Task RemoveAsync(int id);
    void RemoveRange(IEnumerable<TEntity> entities);
    Task SaveChangesAsync();
}