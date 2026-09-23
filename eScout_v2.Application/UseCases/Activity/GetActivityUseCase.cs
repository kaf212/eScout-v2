using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;

namespace eScout_v2.Application.UseCases.Activity;

public class GetActivityUseCase(IRepository<Entities.Activity> activityRepository) : IUseCase<Guid, Entities.Activity>
{
    public async ValueTask<Entities.Activity> ExecuteAsync(Guid input, CancellationToken cancellationToken = default)
    {
        return await activityRepository.GetByIdAsync(input, cancellationToken);
    }
}