using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;

namespace eScout_v2.Application.UseCases.Place;

public class GetPlaceUseCase(IRepository<Entities.Place> placeRepository) : IUseCase<Guid, Entities.Place>
{
    public async ValueTask<Entities.Place> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await placeRepository.GetByIdAsync(id, cancellationToken);
    }
} 