using Microsoft.AspNetCore.Mvc;
using SigmaCars.Application;
using SigmaCars.Application.Features.CarModel;
using SigmaCars.Application.Features.CarModel.Requests;
using SigmaCars.Domain.Models;

namespace SigmaCars.WebApi.Controllers;

[ApiController]
[Route("car-models")]
public class CarModelsController : Controller
{
    private readonly ICarModelsDataService _carModelsDataService;

    public CarModelsController(ICarModelsDataService carModelsDataService)
    {
        _carModelsDataService = carModelsDataService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var carModels = await _carModelsDataService.GetAllAsync();
        return Ok(carModels);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _carModelsDataService.GetAsync(id));
    }
    
    [HttpGet("filtered")]
    public async Task<IActionResult> GetFiltered([FromQuery] GetFilteredCarModelsRequest request)
    {
        return Ok(await _carModelsDataService.GetFilteredAsync(request));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddCarModelRequest request)
    {
        var created = await _carModelsDataService.AddAsync(request);

        return Created($"car-models/{created.Id}", created);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CarModel carModel)
    {
        await _carModelsDataService.UpdateAsync(carModel);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _carModelsDataService.DeleteAsync(id);
        return NoContent();
    }
}