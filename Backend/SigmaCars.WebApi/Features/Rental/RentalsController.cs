using Microsoft.AspNetCore.Mvc;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.Rental;

[ApiController]
[Route("rentals")]
public class RentalsController : ControllerBase
{
    private readonly SigmaCarsDbContext _dbContext;

    public RentalsController(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var rental = await _dbContext.Rentals.FindAsync(id);
        if (rental == null)
        {
            return NotFound();
        }

        _dbContext.Rentals.Remove(rental);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}