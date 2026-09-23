using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;

namespace eScout_v2.Application.UseCases.Place;

public class CreatePlaceUseCase(IRepository<Entities.Place> placeRepository) : IUseCase<PlaceInput, Guid>
{
    public async ValueTask<Guid> ExecuteAsync(PlaceInput input, CancellationToken cancellationToken = default)
    {
        Entities.Place place = Entities.Place.CreateFromInput(input);
        await placeRepository.CreateAsync(place, cancellationToken);
        await placeRepository.SaveChangesAsync(cancellationToken);
        return place.Id;
    }
}