using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigmaCars.Domain.Models;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.Departments;

[ApiController]
[Route("departments")]
public class DepartmentsController : Controller
{
    private readonly SigmaCarsDbContext _dbContext;

    public DepartmentsController(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _dbContext
            .Departments
            .Select(x => new GetResult(x.Id, x.City, x.CountryCode, x.Address))
            .ToListAsync();

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(PostRequest request)
    {
        var department = new Department
        {
            Id = 0,
            CountryCode = request.CountryCode,
            City = request.City,
            Address = request.Address
        };
        await _dbContext.Departments.AddAsync(department);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}