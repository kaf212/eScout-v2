using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace eScout_v2.Application.UseCases.Activity;

public class CreateActivityUseCase(IRepository<Entities.Activity> activityRepository, IServiceScopeFactory serviceScopeFactory) : IUseCase<ActivityInput, Guid>
{
    public async ValueTask<Guid> ExecuteAsync(ActivityInput input, CancellationToken cancellationToken = default)
    {
        Entities.Activity activity = await Entities.Activity.CreateFromInputAsync(input, serviceScopeFactory);
        await activityRepository.CreateAsync(activity, cancellationToken);
        await activityRepository.SaveChangesAsync(cancellationToken);
        return activity.Id;
    }
}