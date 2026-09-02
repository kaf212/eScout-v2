using System.Linq.Expressions;
using eScout_v2.Application.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eScout_v2.Application.Persistence;

public sealed class Repository<TEntity>(DbContext dbContext) : IRepository<TEntity> where TEntity : class
{
    public IQueryable<TEntity> Queryable { get; set; } = dbContext.Set<TEntity>().AsQueryable();

    private DbSet<TEntity> _dbSet { get; set; } = dbContext.Set<TEntity>();
    
    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}