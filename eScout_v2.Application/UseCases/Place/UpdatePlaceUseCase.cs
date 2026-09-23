using eScout_v2.Application.Exceptions;
using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;

namespace eScout_v2.Application.UseCases.Place;

public class UpdatePlaceUseCase(IRepository<Entities.Place> placeRepository) : IUseCaseVoid<PlaceInput>
{
    public async Task ExecuteAsync(PlaceInput input, CancellationToken cancellationToken = default)
    {
        Entities.Place place = await placeRepository.GetByIdAsync(input.Id ?? throw new IdNullException<Entities.Place>(), cancellationToken);
        place.Update(input);
        placeRepository.Update(place);
        await placeRepository.SaveChangesAsync(cancellationToken);
    }
}