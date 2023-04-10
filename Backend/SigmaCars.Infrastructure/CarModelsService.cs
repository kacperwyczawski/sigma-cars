using System.ComponentModel.DataAnnotations;
using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using SigmaCars.Application.Features.CarModel;
using SigmaCars.Application.Features.CarModel.Requests;
using SigmaCars.Domain.Exceptions;
using SigmaCars.Domain.Models;

namespace SigmaCars.Infrastructure;

public class CarModelsService : ICarModelsDataService
{
    private readonly IDbConnection _connection;

    private readonly ILogger<CarModelsService> _logger;

    public CarModelsService(IDbConnection connection, ILogger<CarModelsService> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<CarModel> GetAsync(int id)
    {
        _logger.LogInformation("Attempting to get car model with id {Id}", id);
        var carModel = await _connection.QueryFirstOrDefaultAsync<CarModel?>(
            "select * from car_models where id = @Id",
            new { Id = id });
        return carModel ?? throw new NotFoundException(nameof(CarModel), "get");
    }

    public Task<IEnumerable<CarModel>> GetAsync(GetCarModelRequest request)
    {
        _logger.LogInformation("Attempting to get car models");

        if (request.OrderByPropertyName is not (null or "production_year" or "price_per_day"))
            throw new ValidationException($"{request.OrderByPropertyName} is not a valid property name");

        var sqlBuilder = new SqlBuilder();
        var template = sqlBuilder.AddTemplate(@"
            select * from car_models
            /**where**/
            /**orderby**/");

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

        return _connection.QueryAsync<CarModel>(template.RawSql, template.Parameters);
    }

    public Task<CarModel> CreateAsync(CreateCarModelRequest request)
    {
        _logger.LogInformation("Attempting to add car model with request: {@CarModel}", request);
        return _connection.QueryFirstAsync<CarModel>("""
            insert into car_models (make, model, production_year, color, price_per_day, seat_count)
            values (@Make, @Model, @ProductionYear, @Color, @PricePerDay, @SeatCount)
            returning *
            """, request);
    }

    public async Task UpdateAsync(UpdateCarModelRequest request)
    {
        _logger.LogInformation("Attempting to update car model {@Request}", request);
        var exists = await _connection.QueryFirstAsync<bool>(
            "select exists (select 1 from car_models where id = @Id)", new { request.Id });

        if (!exists)
            throw new NotFoundException(nameof(CarModel), "update");

        await _connection.ExecuteAsync("""
            update car_models
            set make = @Make,
                model = @Model,
                production_year = @ProductionYear,
                color = @Color,
                price_per_day = @PricePerDay,
                seat_count = @SeatCount
            where id = @Id
            """, request);
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInformation("Attempting to delete car model with id {Id}", id);
        var exists = await _connection.QueryFirstAsync<bool>(
            "select exists (select 1 from car_models where id = @Id)", new { Id = id });

        if (!exists)
            throw new NotFoundException(nameof(CarModel), "delete");

        await _connection.ExecuteAsync("delete from car_models where id = @Id", new { Id = id });
    }
}