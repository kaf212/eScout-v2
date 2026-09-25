using FluentValidation;

namespace eScout_v2.Endpoints.Extensions;


public static class EndpointExtensions
{
    private const string ApiBaseRoute = "/api/v1";
    
    public static WebApplicationBuilder AddEndpointExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services
            .AddCors(options =>
            {
                options.AddPolicy("Default", b =>
                {
                    b.WithOrigins(
                            builder.Configuration["App:CorsOrigins"]
                                ?.Split(",", StringSplitOptions.RemoveEmptyEntries)
                                ?? throw new InvalidOperationException("Cors Origins is null")
                        )
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();


        return builder;
    }
    
    public static WebApplication RegisterAllEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup(ApiBaseRoute).WithTags("eScout API v1");
        group.MapActivityEndpoints();
        group.MapPlaceEndpoints();
        return app;
    }

    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)    
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if (validator is null)
            {
                return await next(context).ConfigureAwait(false);
            }

            var model = context.Arguments
                .OfType<T>()
                .FirstOrDefault();

            if (model is null)
            {
                return Results.BadRequest("Invalid request payload.");
            }

            var validationResult = await validator.ValidateAsync(model).ConfigureAwait(false);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            return await next(context).ConfigureAwait(false);
        }
    }
}