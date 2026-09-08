using eScout_v2.Application.Entities;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eScout_v2.Endpoints;

public static class ActivityEndpoints
{
    private const string ActivityRoute = "/activity";
    
    public static RouteGroupBuilder MapActivityEndpoints(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(ActivityRoute, GetAllActivities);
        groupBuilder.MapPost(ActivityRoute, CreateActivity);

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

    private static async Task<IResult> CreateActivity([FromBody] ActivityInput input,
        [FromServices] IUseCase<ActivityInput, Guid> useCase, CancellationToken cancellationToken)
    {
        Guid id = await useCase.ExecuteAsync(input, cancellationToken);
        return Results.Created("agga", id);
    }
}