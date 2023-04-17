using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using SigmaCars.Application.Features.CarModel.Commands;
using SigmaCars.Application.Features.CarModel.Persistence;
using SigmaCars.Application.Features.CarModel.Queries;
using SigmaCars.Domain.Exceptions;
using SigmaCars.Domain.Models;

namespace SigmaCars.Infrastructure.Persistence;

public class CarModelsService : ICarModelsService
{
    private readonly ConnectionString _connectionString;
    private readonly ILogger<CarModelsService> _logger;

    public CarModelsService(
        ConnectionString connectionString,
        ILogger<CarModelsService> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<IEnumerable<CarModel>> GetAsync(GetCarModelsQuery request)
    {
        _logger.LogInformation("Attempting to get car models");

        var sqlBuilder = new SqlBuilder();
        var template = sqlBuilder.AddTemplate(@"select * from car_models /**where**/ /**orderby**/");

        if (request.MinYear != null)
            sqlBuilder.Where("production_year >= @MinYear", new { request.MinYear });
        if (request.MaxYear != null)
            sqlBuilder.Where("production_year <= @MaxYear", new { request.MaxYear });
        if (request.MinPrice != null)
            sqlBuilder.Where("price_per_day >= @MinPrice", new { request.MinPrice });
        if (request.MaxPrice != null)
            sqlBuilder.Where("price_per_day <= @MaxPrice", new { request.MaxPrice });
        if (request.MinSeats != null)
            sqlBuilder.Where("seat_count >= @MinSeats", new { request.MinSeats });
        if (request.MaxSeats != null)
            sqlBuilder.Where("seat_count <= @MaxSeats", new { request.MaxSeats });
        if (request.Make != null)
            sqlBuilder.Where("make = @Make", new { request.Make });
        if (request.Model != null)
            sqlBuilder.Where("model = @Model", new { request.Model });
        if (request.OrderByPropertyName is "production_year" or "price_per_day") // seems to be safe from SQL injection
            sqlBuilder.OrderBy(request.OrderByPropertyName + (request.Ascending ? " asc" : " desc"));

        await using var connection = _connectionString.CreateNpgsqlConnection();
        
        return await connection.QueryAsync<CarModel>(template.RawSql, template.Parameters);
    }

    public async Task<CarModel> CreateAsync(CreateCarModelCommand request)
    {
        _logger.LogInformation("Attempting to add car model with request: {@CarModel}", request);


        await using var connection = _connectionString.CreateNpgsqlConnection();
        
        return await connection.QueryFirstAsync<CarModel>("""
            insert into car_models (make, model, production_year, color, price_per_day, seat_count)
            values (@Make, @Model, @ProductionYear, @Color, @PricePerDay, @SeatCount)
            returning *
            """, request);
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation("Attempting to delete car model with id {Id}", id);

        if (!await ExistsAsync(id))
            throw new NotFoundException(nameof(CarModel), "delete");

        await using var connection = _connectionString.CreateNpgsqlConnection();
        
        await connection.ExecuteAsync("delete from car_models where id = @Id", new { Id = id });
    }

    private async Task<bool> ExistsAsync(int id)
    {
        await using var connection = _connectionString.CreateNpgsqlConnection();
        
        return await connection.QueryFirstAsync<bool>(
            "select exists (select 1 from car_models where id = @Id)", new { Id = id });
    }
}