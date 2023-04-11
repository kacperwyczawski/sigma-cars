using System.Data;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Npgsql;
using SigmaCars.Application.Features.CarModel;
using SigmaCars.Application.Features.CarModel.Requests;
using SigmaCars.Infrastructure;
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
builder.Services.AddTransient<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<ICarModelsDataService, CarModelsService>();

// Middlewares
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// Fluent validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateCarModelRequestValidator>(ServiceLifetime.Transient);


var app = builder.Build();

app.UseSwagger(options => options.RouteTemplate = "schema/{documentName}");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();