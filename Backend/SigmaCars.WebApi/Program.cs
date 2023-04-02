using System.Data;
using Npgsql;
using SigmaCars.Application;
using SigmaCars.Domain;
using SigmaCars.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICarModelsDataService, CarModelsDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.MapGet("/car-models", async (ICarModelsDataService carModelsDataService) => 
    await carModelsDataService.GetAllAsync());

app.MapPost("/car-models", async (ICarModelsDataService carModelsDataService, CarModel carModel) =>
        await carModelsDataService.AddAsync(carModel));

app.Run();