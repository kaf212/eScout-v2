using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.Logging;

namespace eScout_v2.Application.UseCases.Place;

public class CreatePlaceUseCase(
    IRepository<Entities.Place> placeRepository,
    ILogger<CreatePlaceUseCase> logger
    ) : IUseCase<PlaceInput, Guid>
{
    public async ValueTask<Guid> ExecuteAsync(PlaceInput id, CancellationToken cancellationToken = default)
    {
        Entities.Place place = Entities.Place.CreateFromInput(id);
        await placeRepository.CreateAsync(place, cancellationToken);
        await placeRepository.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Successfully created place with ID {id}.", place.Id);
        return place.Id;
    }
}