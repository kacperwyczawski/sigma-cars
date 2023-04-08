using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using SigmaCars.Application;
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
        var carModel = await _connection.QueryFirstOrDefaultAsync<CarModel?>("select * from car_models where id = @Id",
            new { Id = id });
        return carModel ?? throw new NotFoundException(nameof(CarModel), "get");
    }

    public Task<IEnumerable<CarModel>> GetAllAsync()
    {
        _logger.LogInformation("Attempting to get all car models");
        return _connection.QueryAsync<CarModel>("select * from car_models");
    }

    public Task<IEnumerable<CarModel>> GetFilteredAsync(GetFilteredCarModelsRequest request)
    {
        var make = request.Make ?? "";
        var model = request.Model ?? "";
        var minYear = request.MinYear ?? 0;
        var maxYear = request.MaxYear ?? int.MaxValue;
        var minPrice = request.MinPrice ?? 0;
        var maxPrice = request.MaxPrice ?? float.MaxValue;
        var minSeats = request.MinSeats ?? 0;
        var maxSeats = request.MaxSeats ?? int.MaxValue;
        
        _logger.LogInformation(
            "Attempting to get car models with filters: " +
            "make = {Make}, model = {Model}, " +
            "production year = {MinYear}-{MaxYear}, " +
            "price per day = {MinPrice}-{MaxPrice}, " +
            "seat count = {MinSeats}-{MaxSeats}",
            make, model, minYear, maxYear, minPrice, maxPrice, minSeats, maxSeats);

        return _connection.QueryAsync<CarModel>("""
            select * from car_models
            where
                make like @Make
                and model like @Model
                and production_year between @MinYear and @MaxYear
                and price_per_day between @MinPrice and @MaxPrice
                and seat_count between @MinSeats and @MaxSeats
            """, new
        {
            Make = $"%{make}%",
            Model = $"%{model}%",
            MinYear = minYear,
            MaxYear = maxYear,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            MinSeats = minSeats,
            MaxSeats = maxSeats
        });
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