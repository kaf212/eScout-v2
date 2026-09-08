using eScout_v2.Application.Entities;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eScout_v2.Endpoints;

public static class PlaceEndpoints
{
    private const string PlaceRoute = "/place";
    
    public static RouteGroupBuilder MapPlaceEndpoints(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(PlaceRoute, GetAllPlaces);
        groupBuilder.MapPost(PlaceRoute, CreatePlace);
        return groupBuilder;
    }

    private static async Task<IResult> GetAllPlaces([FromServices] IUseCase<IEnumerable<Place>> useCase,
        CancellationToken cancellationToken)
    {
        IEnumerable<Place> places = await useCase.ExecuteAsync(cancellationToken);
        if (places.Count() == 0)
        {
            return Results.NoContent();
        }

        return Results.Ok(places);
    }

    private static async Task<IResult> CreatePlace([FromBody] PlaceInput input,
        [FromServices] IUseCase<PlaceInput, Guid> useCase, CancellationToken cancellationToken)
    {
        Guid id = await useCase.ExecuteAsync(input, cancellationToken);
        return Results.Created("agga", id);
    }
}