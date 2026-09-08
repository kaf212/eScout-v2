using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using Microsoft.Extensions.DependencyInjection;

namespace eScout_v2.Application.Entities;

public record Activity(Guid Id, string Title, string Description, Guid PlaceId, DateTime StartTime, DateTime EndTime)
{
    public Place Place { get; init; }
    
    public static async Task<Activity> CreateFromInputAsync(ActivityInput input, IServiceScopeFactory serviceScopeFactory)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IRepository<Place> placeRepository = scope.ServiceProvider.GetService<IRepository<Place>>() ??
                                              throw new InvalidOperationException(
                                                  "Could not get IRepository<Place> from ServicePrvider.");
        
        Place place = await placeRepository.GetByIdAsync(input.PlaceId);

        return new(Guid.NewGuid(), input.Title, input.Description, place.Id, input.StartTime, input.EndTime) 
            { Place = place };
    }
}