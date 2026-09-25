using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.Logging;

namespace eScout_v2.Application.UseCases.Activity;

public class GetActivityUseCase(
    IRepository<Entities.Activity> activityRepository,
    ILogger<GetActivityUseCase> logger
) : IUseCase<Guid, Entities.Activity>
{
    public async ValueTask<Entities.Activity> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Entities.Activity activity = await activityRepository.GetByIdAsync(id, cancellationToken);
        logger.LogInformation("Successfully retrieved activity with ID {id} from database.", id);
        return activity;
    }
}