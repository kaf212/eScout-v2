using eScout_v2.Application.Exceptions;
using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;

namespace eScout_v2.Application.UseCases.Activity;

public class UpdateActivityUseCase(IRepository<Entities.Activity> activityRepository) : IUseCaseVoid<ActivityInput>
{
    public async Task ExecuteAsync(ActivityInput input, CancellationToken cancellationToken = default)
    {
        Entities.Activity activity = await activityRepository.GetByIdAsync(input.Id ?? throw new IdNullException<Entities.Activity>(), cancellationToken);
        activity.Update(input);
        await activityRepository.SaveChangesAsync(cancellationToken);
    }
}