using eScout_v2.Application.UseCases.Inputs;

namespace eScout_v2.Application.Entities;

public record Place
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Place CreateFromInput(PlaceInput input)
    {
        return new Place
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Description = input.Description
        };
    }

    public void Update(PlaceInput input)
    {
        Name = input.Name;
        Description = input.Description;
    }
}