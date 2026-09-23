using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;

namespace eScout_v2.Application.UseCases.Activity;

public class DeleteActivityUseCase(IRepository<Entities.Activity> activityRepository) : IUseCaseVoid<Guid>
{
    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Entities.Activity activity = await activityRepository.GetByIdAsync(id, cancellationToken);
        activityRepository.Delete(activity);
        await activityRepository.SaveChangesAsync(cancellationToken);
    }
}