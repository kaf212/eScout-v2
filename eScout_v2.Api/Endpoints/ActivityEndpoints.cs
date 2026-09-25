using eScout_v2.Application.Entities;
using eScout_v2.Application.UseCases.Activity;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;
using eScout_v2.Endpoints.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace eScout_v2.Endpoints;

public static class ActivityEndpoints
{
    private const string ActivityRoute = "/activity";
    
    public static RouteGroupBuilder MapActivityEndpoints(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(ActivityRoute, GetAllActivities);
        
        groupBuilder.MapGet($"{ActivityRoute}/{{id:guid}}", GetActivity);

        groupBuilder
            .MapPost(ActivityRoute, CreateActivity)
            .AddEndpointFilter<EndpointExtensions.ValidationFilter<ActivityInput>>();
            
        groupBuilder
            .MapPut($"{ActivityRoute}/{{id:guid}}", UpdateActivity)
            .AddEndpointFilter<EndpointExtensions.ValidationFilter<ActivityInput>>();
        
        groupBuilder.MapDelete($"{ActivityRoute}/{{id:guid}}", DeleteActivity);

        return groupBuilder;
    }

    private static async Task<IResult> GetAllActivities([FromServices] IUseCase<IEnumerable<Activity>> useCase, CancellationToken cancellationToken)
    {
        IEnumerable<Activity> activties = await useCase.ExecuteAsync(cancellationToken);
        if (activties.Count() == 0)
        {
            return Results.NoContent();
        }
        return Results.Ok(activties);
    }
    
    private static async Task<IResult> GetActivity([FromRoute] Guid id, [FromServices] IUseCase<Guid, Activity> useCase, CancellationToken cancellationToken)
    {
        Activity activties = await useCase.ExecuteAsync(id, cancellationToken);
        return Results.Ok(activties);
    }

    private static async Task<IResult> CreateActivity([FromBody] ActivityInput input,
        [FromServices] IUseCase<ActivityInput, Guid> useCase, CancellationToken cancellationToken)
    {
        Guid id = await useCase.ExecuteAsync(input, cancellationToken);
        return Results.Created("agga", id);
    }
    
    private static async Task<IResult> UpdateActivity([FromRoute] Guid id, [FromBody] ActivityInput input,
        [FromServices] IUseCaseVoid<ActivityInput> useCase, CancellationToken cancellationToken)
    {
        await useCase.ExecuteAsync(input with {Id = id}, cancellationToken);
        return Results.Created();
    }
    
    private static async Task<IResult> DeleteActivity([FromRoute] Guid id, [FromKeyedServices(nameof(DeleteActivityUseCase))] IUseCaseVoid<Guid> useCase, CancellationToken cancellationToken)
    {
        await useCase.ExecuteAsync(id, cancellationToken);
        return Results.Ok();
    }
}