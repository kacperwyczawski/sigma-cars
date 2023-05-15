using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SigmaCars.Domain.Models;
using SigmaCars.WebApi.Features.Car.Commands;
using SigmaCars.WebApi.Features.Car.Queries;
using SigmaCars.WebApi.Features.CarModel.Commands;
using SigmaCars.WebApi.Features.CarModel.Queries;

namespace SigmaCars.WebApi.Features.CarModel;

[ApiController]
[Route("car-models")]
public class CarModelsController : Controller
{
    private readonly IMediator _mediator;

    public CarModelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery(Name = "start-date")] DateTime startDate,
        [FromQuery(Name = "end-date")] DateTime endDate,
        [FromQuery(Name = "min-year")] int? minYear,
        [FromQuery(Name = "max-year")] int? maxYear,
        [FromQuery(Name = "min-price")] float? minPrice,
        [FromQuery(Name = "max-price")] float? maxPrice,
        [FromQuery(Name = "min-seats")] int? minSeats,
        [FromQuery(Name = "max-seats")] int? maxSeats,
        [FromQuery(Name = "make")] string? make,
        [FromQuery(Name = "model")] string? model,
        [FromQuery(Name = "order-by")] string? orderByPropertyName,
        [FromQuery(Name = "ascending")] bool ascending = true,
        [FromQuery(Name = "available-only")] bool availableOnly = true)
    {
        var request = new GetCarModelsQuery(
            startDate, endDate,
            minYear, maxYear,
            minPrice, maxPrice,
            minSeats, maxSeats,
            make, model,
            orderByPropertyName, ascending,
            availableOnly);

        var result = await _mediator.Send(request);

        if (!result.CarModels.Any())
            return NoContent();

        return Ok(result);
    }
    
    [HttpGet("{id:int}/cars")]
    public async Task<IActionResult> GetCars(int id)
    {
        var request = new GetCarsQuery(id);

        var result = await _mediator.Send(request);

        if (!result.Cars.Any())
            return NoContent();

        return Ok(result);
    }
    
    [HttpDelete("{id:int}/cars/{carId:int}")]
    public async Task<IActionResult> DeleteCar(int id, int carId)
    {
        await _mediator.Send(new DeleteCarCommand(carId));

        return NoContent();
    }
    
    [HttpPost("{id:int}/cars")]
    public async Task<IActionResult> PostCar(int id, [FromBody] CreateCarCommand request)
    {
        var created = await _mediator.Send(request);

        return Created($"car-models/{id}/cars/{created.Id}", created);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCarModelCommand request)
    {
        var created = await _mediator.Send(request);

        return Created($"car-models/{created.Id}", created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteCarModelCommand(id));

        return NoContent();
    }
}