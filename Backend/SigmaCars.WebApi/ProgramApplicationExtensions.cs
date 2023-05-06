using SigmaCars.WebApi.Middlewares;

namespace SigmaCars.WebApi;

internal static class ProgramApplicationExtensions
{
    internal static void UseCustomSwagger(this WebApplication app) =>
        app.UseSwagger(options => options.RouteTemplate = "schema/{documentName}");

    internal static void UseExceptionHandling(this WebApplication app) =>
        app.UseMiddleware<ExceptionHandlingMiddleware>();
}