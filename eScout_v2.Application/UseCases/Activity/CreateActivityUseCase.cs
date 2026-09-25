using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.Logging;

namespace eScout_v2.Application.UseCases.Activity;

public class CreateActivityUseCase(
    IRepository<Entities.Activity> activityRepository,
    ILogger<CreateActivityUseCase> logger
    ) : IUseCase<ActivityInput, Guid>
{
    public async ValueTask<Guid> ExecuteAsync(ActivityInput id, CancellationToken cancellationToken = default)
    {
        Entities.Activity activity = await Entities.Activity.CreateFromInputAsync(id);
        await activityRepository.CreateAsync(activity, cancellationToken);
        await activityRepository.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Successfully created activity with ID {id}", activity.Id);
        return activity.Id;
    }
}