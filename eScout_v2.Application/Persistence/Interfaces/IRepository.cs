using System.Linq.Expressions;

namespace eScout_v2.Application.Persistence.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    public Task CreateAsync(TEntity entity, CancellationToken cancellationToken);

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}