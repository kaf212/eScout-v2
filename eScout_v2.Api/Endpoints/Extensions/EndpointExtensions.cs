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


        return builder;
    }
    
    public static WebApplication RegisterAllEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup(ApiBaseRoute).WithTags("eScout API v1");
        group.MapActivityEndpoints();
        group.MapPlaceEndpoints();
        return app;
    }
}