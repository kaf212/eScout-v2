using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using Microsoft.Extensions.DependencyInjection;

namespace eScout_v2.Application.Entities;

public record Activity(Guid Id, string Title, string Description, Guid PlaceId, DateTime StartTime, DateTime EndTime)
{
    public Place Place { get; init; }
    
    public static async Task<Activity> CreateFromInputAsync(ActivityInput input)
    {
        return new(Guid.NewGuid(), input.Title, input.Description, input.PlaceId, input.StartTime, input.EndTime);
    }
}