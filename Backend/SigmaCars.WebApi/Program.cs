using System.Data;
using Microsoft.OpenApi.Models;
using Npgsql;
using SigmaCars.Application;
using SigmaCars.Application.Features.CarModel;
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

builder.Services.AddTransient<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICarModelsDataService, CarModelsService>();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();


var app = builder.Build();

app.UseSwagger(options => options.RouteTemplate = "schema/{documentName}");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();