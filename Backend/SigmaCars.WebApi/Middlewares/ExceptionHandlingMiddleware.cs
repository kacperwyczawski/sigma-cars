using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SigmaCars.Domain.Exceptions;

namespace SigmaCars.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogInformation("Handling exception: {Exception}", exception.GetType().Name);
            
            var statusCode = HttpStatusCode.InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Title = "Unexpected server error",
                Detail = exception.Message
            };

            switch (exception)
            {
                case NotFoundException e:
                    statusCode = HttpStatusCode.NotFound;
                    problemDetails.Title = "Not found";
                    problemDetails.Detail = e.Details;
                    break;
                case ValidationException e:
                    statusCode = HttpStatusCode.BadRequest;
                    problemDetails.Title = "Validation error";
                    problemDetails.Detail = e.Message;
                    break;
            }

            problemDetails.Status = (int)statusCode;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}