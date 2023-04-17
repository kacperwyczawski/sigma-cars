using System.Data;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using Npgsql;
using SigmaCars.Application.Behaviors;
using SigmaCars.Application.Features.Car.Persistence;
using SigmaCars.Application.Features.CarModel.Commands;
using SigmaCars.Application.Features.CarModel.Persistence;
using SigmaCars.Infrastructure.Persistence;
using SigmaCars.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sigma cars web API",
        Description = "Backend API for Sigma cars web application",
        License = new OpenApiLicense
        {
            Name = "GNU AGPLv3",
            Url = new Uri("https://github.com/kacperwyczawski/sigma-cars/blob/main/LICENSE")
        }
    });
});

// Database
builder.Services.AddTransient<ConnectionString>(_ =>
    new ConnectionString(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not found")));

// Services
builder.Services.AddScoped<ICarModelsService, CarModelsService>();
builder.Services.AddScoped<ICarsService, CarsService>();

// Middlewares
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Fluent validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateCarModelValidator>(ServiceLifetime.Transient);

// Mediator
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreateCarModelHandler>());
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

app.UseSwagger(options => options.RouteTemplate = "schema/{documentName}");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();