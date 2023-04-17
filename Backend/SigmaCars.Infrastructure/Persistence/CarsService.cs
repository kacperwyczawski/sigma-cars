using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using SigmaCars.Application.Features.Car.Persistence;

namespace SigmaCars.Infrastructure.Persistence;

public class CarsService : ICarsService
{
    private readonly ConnectionString _connectionString;
    private readonly ILogger<CarsService> _logger;

    public CarsService(
        ConnectionString connectionString,
        ILogger<CarsService> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<IEnumerable<int>> GetAvailableCarsIdsAsync(int carModelId, DateTime startDate, DateTime endDate)
    {
        _logger.LogInformation("Attempting to get available cars ids");

        await using var connection = _connectionString.CreateNpgsqlConnection();
        
        return await connection.QueryAsync<int>("""
            select c.id
            from cars c
            where c.car_model_id = @CarModelId
                and c.id not in (
                    select r.car_id
                    from rentals r
                    where r.car_id = c.id
                        and r.start_date <= @EndDate
                        and r.end_date >= @StartDate
            )
            """, new { CarModelId = carModelId, StartDate = startDate, EndDate = endDate });
    }

    public async Task<IEnumerable<int>> GetCarIdsAsync(int carModelId)
    {
        _logger.LogInformation("Attempting to get cars ids for car model {CarModelId}", carModelId);

        await using var connection = _connectionString.CreateNpgsqlConnection();
        
        return await connection.QueryAsync<int>("""
                        select c.id
                        from cars c
                        where c.car_model_id = @CarModelId
                        """, new { CarModelId = carModelId });
    }
}