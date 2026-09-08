using eScout_v2.Application.Entities;
using eScout_v2.Application.UseCases.Activity;
using eScout_v2.Application.UseCases.Extensions;
using eScout_v2.Application.UseCases.Inputs;
using eScout_v2.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eScout_v2.Application.Extensions;

public static class ApplicationExtensions
{
    public static WebApplicationBuilder AddApplicationExtensions(this WebApplicationBuilder builder)
    {
        // builder.Services.AddUseCases();
        builder.Services.AddScoped<IUseCase<IEnumerable<Activity>>, GetAllActivitiesUseCase>();
        builder.Services.AddScoped<IUseCase<ActivityInput, Guid>, CreateActivityUseCase>();
        return builder;
    }
}