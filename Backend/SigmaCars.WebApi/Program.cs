using System.Data;
using Microsoft.OpenApi.Models;
using Npgsql;
using SigmaCars.Application;
using SigmaCars.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<ICarModelsDataService, CarModelsDataService>();


var app = builder.Build();

app.UseSwagger(options =>
{
    options.RouteTemplate = "schema/{documentName}";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();