using eScout_v2.Application.Infrastructure.Database.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eScout_v2.Application.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static WebApplicationBuilder AddInfrastructureExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<AppDbContext>());

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("Escout")));

        return builder;
    }
}