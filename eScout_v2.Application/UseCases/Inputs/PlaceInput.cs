namespace eScout_v2.Application.UseCases.Inputs;

public record PlaceInput
{
    public required Guid? Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}