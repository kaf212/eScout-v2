using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.Logging;

namespace eScout_v2.Application.UseCases.Place;

public class GetAllPlacesUseCase(
    IRepository<Entities.Place> placeRepository,
    ILogger<GetAllPlacesUseCase> logger
    ) : IUseCase<IEnumerable<Entities.Place>>
{
    public async ValueTask<IEnumerable<Entities.Place>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Entities.Place> places = await placeRepository.GetAllAsync(null, cancellationToken);
        logger.LogInformation("Successfully retrieved {count} places from database.", places.Count());
        return places;
    }
}   