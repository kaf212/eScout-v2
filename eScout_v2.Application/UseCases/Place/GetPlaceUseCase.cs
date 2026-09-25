using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.Logging;

namespace eScout_v2.Application.UseCases.Place;

public class GetPlaceUseCase(
    IRepository<Entities.Place> placeRepository,
    ILogger<GetPlaceUseCase> logger
    ) : IUseCase<Guid, Entities.Place>
{
    public async ValueTask<Entities.Place> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Entities.Place place = await placeRepository.GetByIdAsync(id, cancellationToken);
        logger.LogInformation("Successfully retrieved place with ID {id} from database.", id);
        return place;
    }
} 