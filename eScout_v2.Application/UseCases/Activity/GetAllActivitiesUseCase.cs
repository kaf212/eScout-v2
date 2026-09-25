using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eScout_v2.Application.UseCases.Activity;

public class GetAllActivitiesUseCase(
    IRepository<Entities.Activity> activityRepository,
    ILogger<GetAllActivitiesUseCase> logger
    ) : IUseCase<IEnumerable<Entities.Activity>>
{
    public async ValueTask<IEnumerable<Entities.Activity>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Entities.Activity> activities = await activityRepository.GetAllAsync(null, cancellationToken);
        logger.LogInformation("Successfully retrieved {count} activities from database.", activities.Count());
        return activities;
    }
}