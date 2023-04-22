using System.Net;
using System.Text.Json;
using FluentValidation;
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
            _logger.LogInformation("Handling {Exception}", exception.GetType());
            
            var statusCode = HttpStatusCode.InternalServerError;
            var problemDetails = new ProblemDetails
            {
                Title = "Unexpected server error"
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
                case ConflictException e:
                    statusCode = HttpStatusCode.Conflict;
                    problemDetails.Title = "Conflict";
                    problemDetails.Detail = e.Details;
                    break;
                case InvalidCredentialsException e:
                    statusCode = HttpStatusCode.Unauthorized;
                    problemDetails.Title = "Invalid credentials";
                    problemDetails.Detail = e.Details;
                    break;
            }

            // if exception is not expected
            if ((int)statusCode == 500)
                _logger.LogError(exception, "Unexpected exception");

            problemDetails.Status = (int)statusCode;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}