using Challenge.Application.Exceptions;
using Challenge.Application.Interfaces.Repository;
using Challenge.Domain.Models;
using Challenge.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Challenge.Infrastructure.Data.Repository;

[ExcludeFromCodeCoverage]
public abstract class BaseRepository<TEntity>(SqlDbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly SqlDbContext _context = context;

    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public virtual void Update(TEntity entity)
        => _dbSet.Update(entity);

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        => await _dbSet.AddRangeAsync(entities);

    public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression)
        => _dbSet.Where(expression);

    public virtual async Task<IEnumerable<TEntity>> GetAsllListAsync()
        => await _dbSet.ToListAsync();

    public virtual IQueryable<TEntity> GetAllAsync()
        => _dbSet.AsQueryable();

    public virtual Pagination<TEntity> FindAllPaginate(Expression<Func<TEntity, bool>> expression, int offset, int limit = 100)
    {
        var query = _dbSet.Where(expression).AsQueryable();
        return Paginate(query, offset, limit);
    }

    public virtual Pagination<TEntity> Paginate(IQueryable<TEntity> query, int offset, int limit = 100)
        => new()
        {
            Offset = offset,
            Limit = limit,
            Total = query.Count(),
            Itens = [.. query.Skip(offset).Take(limit)]
        };

    public virtual async Task<TEntity> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id) ?? throw new NullEntityException("Entity not found");

    public virtual void Remove(TEntity entity)
        => _dbSet.Remove(entity);

    public virtual async Task RemoveAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
        => _dbSet.RemoveRange(entities);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}