using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace eScout_v2.Middleware.Handlers;
public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                IExceptionHandlerFeature? errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (errorFeature is null)
                {
                    return;
                }

                Exception exception = errorFeature.Error;

                ILogger<Program> logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

                HttpStatusCode code = exception switch
                {
                    BadHttpRequestException => HttpStatusCode.BadRequest,
                    ValidationException => HttpStatusCode.BadRequest,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    _ => HttpStatusCode.InternalServerError,
                };

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int)code;

                ProblemDetails problem = new ProblemDetails
                {
                    Title = code.ToString(),
                    Detail = exception.Message,
                    Status = (int)code,
                    Instance = context.Request.Path,
                };

                problem.Extensions["traceId"] = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;

                string payload = JsonSerializer.Serialize(problem,
                    context.RequestServices.GetRequiredService<JsonSerializerOptions>());

                await context.Response.WriteAsync(payload).ConfigureAwait(false);
            });
        });
        return app;
    }
}
