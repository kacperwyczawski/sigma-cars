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

    public Task<IEnumerable<CarModel>> GetAsync(
        int? minYear, int? maxYear,
        float? minPrice, float? maxPrice,
        int? minSeats, int? maxSeats,
        string? make,
        string? model,
        string? orderByPropertyName,
        bool ascending = true)
    {
        _logger.LogInformation("Attempting to get car models");

        if (orderByPropertyName is not (null or "production_year" or "price_per_day"))
            throw new ValidationException($"{orderByPropertyName} is not a valid property name");

        var sqlBuilder = new SqlBuilder();
        var template = sqlBuilder.AddTemplate(@"
            select * from car_models
            /**where**/
            /**orderby**/");

        if (minYear != null)
            sqlBuilder.Where("production_year >= @MinYear", new { MinYear = minYear });
        if (maxYear != null)
            sqlBuilder.Where("production_year <= @MaxYear", new { MaxYear = maxYear });
        if (minPrice != null)
            sqlBuilder.Where("price_per_day >= @MinPrice", new { MinPrice = minPrice });
        if (maxPrice != null)
            sqlBuilder.Where("price_per_day <= @MaxPrice", new { MaxPrice = maxPrice });
        if (minSeats != null)
            sqlBuilder.Where("seat_count >= @MinSeats", new { MinSeats = minSeats });
        if (maxSeats != null)
            sqlBuilder.Where("seat_count <= @MaxSeats", new { MaxSeats = maxSeats });
        if (make != null)
            sqlBuilder.Where("make = @Make", new { Make = make });
        if (model != null)
            sqlBuilder.Where("model = @Model", new { Model = model });
        if (orderByPropertyName is "production_year" or "price_per_day") // seems to be safe from SQL injection
            sqlBuilder.OrderBy(orderByPropertyName + (ascending ? " asc" : " desc"));

        return _connection.QueryAsync<CarModel>(template.RawSql, template.Parameters);
    }

    public Task<CarModel> AddAsync(AddCarModelRequest request)
    {
        _logger.LogInformation("Attempting to add car model with request: {@CarModel}", request);
        return _connection.QueryFirstAsync<CarModel>("""
            insert into car_models (make, model, production_year, color, price_per_day, seat_count)
            values (@Make, @Model, @ProductionYear, @Color, @PricePerDay, @SeatCount)
            returning *
            """, request);
    }

    public async Task UpdateAsync(CarModel carModel)
    {
        _logger.LogInformation("Attempting to update car model {@CarModel}", carModel);
        var exists = await _connection.QueryFirstAsync<bool>(
            "select exists (select 1 from car_models where id = @Id)", new { carModel.Id });

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
            """, carModel);
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