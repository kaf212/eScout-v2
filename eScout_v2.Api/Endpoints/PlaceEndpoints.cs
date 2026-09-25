using eScout_v2.Application.Entities;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;
using eScout_v2.Application.UseCases.Place;
using eScout_v2.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace eScout_v2.Endpoints;

public static class PlaceEndpoints
{
    private const string PlaceRoute = "/place";
    
    public static RouteGroupBuilder MapPlaceEndpoints(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(PlaceRoute, GetAllPlaces);
        
        groupBuilder.MapGet($"{PlaceRoute}/{{id:guid}}", GetPlace);
        
        groupBuilder
            .MapPost(PlaceRoute, CreatePlace)
            .AddEndpointFilter<EndpointExtensions.ValidationFilter<PlaceInput>>();
        
        groupBuilder
            .MapPut($"{PlaceRoute}/{{id:guid}}", UpdatePlace)
            .AddEndpointFilter<EndpointExtensions.ValidationFilter<PlaceInput>>();
        
        groupBuilder.MapDelete($"{PlaceRoute}/{{id:guid}}", DeletePlace);
        
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
    
    private static async Task<IResult> GetPlace([FromRoute] Guid id, [FromServices] IUseCase<Guid, Place> useCase,
        CancellationToken cancellationToken)
    {
        Place place = await useCase.ExecuteAsync(id, cancellationToken);
        return Results.Ok(place);
    }

    private static async Task<IResult> CreatePlace([FromBody] PlaceInput input,
        [FromServices] IUseCase<PlaceInput, Guid> useCase, CancellationToken cancellationToken)
    {
        Guid id = await useCase.ExecuteAsync(input, cancellationToken);
        return Results.Created("agga", id);
    }

    private static async Task<IResult> UpdatePlace([FromRoute] Guid id, [FromBody] PlaceInput input,
        [FromServices] IUseCaseVoid<PlaceInput> useCase, CancellationToken cancellationToken)
    {
        await useCase.ExecuteAsync(input with {Id = id}, cancellationToken);
        return Results.Created();
    }
    
    private static async Task<IResult> DeletePlace([FromRoute] Guid id,
        [FromKeyedServices(nameof(DeletePlaceUseCase))] IUseCaseVoid<Guid> useCase, CancellationToken cancellationToken)
    {
        await useCase.ExecuteAsync(id, cancellationToken);
        return Results.Created();
    }
}