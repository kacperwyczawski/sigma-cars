using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            .Select(x => new GetResult(x.Id, x.City))
            .ToListAsync();

        if (!result.Any())
            return NoContent();

        return Ok(result);
    }
}