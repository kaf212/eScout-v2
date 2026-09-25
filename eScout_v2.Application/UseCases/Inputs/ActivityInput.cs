using eScout_v2.Application.Entities;

namespace eScout_v2.Application.UseCases.Inputs;

public record ActivityInput
{
    public Guid? Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required Guid PlaceId { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
}