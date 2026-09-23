using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eScout_v2.Application.UseCases.Activity;

public class GetAllActivitiesUseCase(IRepository<Entities.Activity> activityRepository) : IUseCase<IEnumerable<Entities.Activity>>
{
    public async ValueTask<IEnumerable<Entities.Activity>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        IQueryable<Entities.Activity> queryable = activityRepository.Queryable;

        return queryable
            .Include(a => a.Place)
            .AsEnumerable();
    }
}