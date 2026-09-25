using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.Extensions.Logging;

namespace eScout_v2.Application.UseCases.Place;

public class DeletePlaceUseCase(
    IRepository<Entities.Place> placeRepository,
    ILogger<DeletePlaceUseCase> logger
    ) : IUseCaseVoid<Guid>
{
    public async Task ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Entities.Place place = await placeRepository.GetByIdAsync(id, cancellationToken);
        placeRepository.Delete(place);
        await placeRepository.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Place with ID {id} has been successfully deleted.", place.Id);
    }
}