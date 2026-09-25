using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.Logging;

namespace eScout_v2.Application.UseCases.Activity;

public class DeleteActivityUseCase(
    IRepository<Entities.Activity> activityRepository,
    ILogger<DeleteActivityUseCase> logger
    ) : IUseCaseVoid<Guid>
{
    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Entities.Activity activity = await activityRepository.GetByIdAsync(id, cancellationToken);
        activityRepository.Delete(activity);
        await activityRepository.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Successfully deleted activity with ID {id}.", id);
    }
}