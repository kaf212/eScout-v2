using eScout_v2.Application.UseCases.Inputs;

namespace eScout_v2.Application.Entities;

public record Place(Guid Id, string Name, string Description)
{
    public static Place CreateFromInput(PlaceInput input)
    {
        return new Place(Guid.NewGuid(), input.Name, input.Description);
    }
}