using System.Linq.Expressions;
using eScout_v2.Application.Exceptions;
using eScout_v2.Application.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eScout_v2.Application.Persistence;

public sealed class Repository<TEntity>(DbContext dbContext) : IRepository<TEntity> where TEntity : class
{
    public IQueryable<TEntity> Queryable { get; set; } = dbContext.Set<TEntity>().AsQueryable();

    private DbSet<TEntity> _dbSet { get; set; } = dbContext.Set<TEntity>();
    
    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _dbSet.FindAsync([id], cancellationToken).ConfigureAwait(false);
        if (entity is null)
        {
            throw new EntityNotFoundException<TEntity>(id);
        }
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate != null)
        {
            return await Queryable.Where(predicate).ToListAsync(cancellationToken);
        }
        return await Queryable.ToListAsync(cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}