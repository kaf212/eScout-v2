using System.Linq.Expressions;
using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Interfaces;

namespace eScout_v2.Application.UseCases.Place;

public class GetAllPlacesUseCase(IRepository<Entities.Place> placeRepository) : IUseCase<IEnumerable<Entities.Place>>
{
    public async ValueTask<IEnumerable<Entities.Place>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await placeRepository.GetAllAsync(null, cancellationToken);
    }
}   