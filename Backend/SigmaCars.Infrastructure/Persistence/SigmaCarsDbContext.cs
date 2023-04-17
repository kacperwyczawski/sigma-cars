using Microsoft.EntityFrameworkCore;
using SigmaCars.Domain.Models;

namespace SigmaCars.Infrastructure.Persistence;

public class SigmaCarsDbContext : DbContext
{
    public SigmaCarsDbContext(DbContextOptions<SigmaCarsDbContext> options) : base(options)
    {
    }
    
    public DbSet<Car> Cars { get; set; }
    
    public DbSet<CarModel> CarModels { get; set; }
    
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<Rental> Rentals { get; set; }
    
    public DbSet<User> Users { get; set; }
}