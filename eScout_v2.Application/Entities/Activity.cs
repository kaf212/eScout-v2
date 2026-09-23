using eScout_v2.Application.Persistence.Interfaces;
using eScout_v2.Application.UseCases.Inputs;
using Microsoft.Extensions.DependencyInjection;

namespace eScout_v2.Application.Entities;

public record Activity
{
    public Guid Id { get; init; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid PlaceId { get; private set; }
    public Place Place { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    
    public static async Task<Activity> CreateFromInputAsync(ActivityInput input)
    {
        return new Activity
        {
            Id = Guid.NewGuid(),
            Title = input.Title,
            Description = input.Description,
            PlaceId = input.PlaceId,
            StartTime = input.StartTime,
            EndTime = input.EndTime
        };
    }

    public void Update(ActivityInput input)
    {
        Title = input.Title;
        Description = input.Description;
        PlaceId = input.PlaceId;
        StartTime = input.StartTime;
        EndTime = input.EndTime;
    }
}