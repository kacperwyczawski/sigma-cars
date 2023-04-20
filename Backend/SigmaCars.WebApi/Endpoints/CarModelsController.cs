using MediatR;
using Microsoft.AspNetCore.Mvc;
using SigmaCars.Application.Features.CarModel.Commands;
using SigmaCars.Application.Features.CarModel.Queries;

namespace SigmaCars.WebApi.Endpoints;

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
        [FromQuery(Name = "ascending")] bool ascending = true)
    {
        var request = new GetCarModelsQuery(
            startDate, endDate,
            minYear, maxYear,
            minPrice, maxPrice,
            minSeats, maxSeats,
            make,
            model,
            orderByPropertyName,
            ascending);

        var result = await _mediator.Send(request);

        if (!result.Any())
            return NoContent();
        
        return Ok(result);
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