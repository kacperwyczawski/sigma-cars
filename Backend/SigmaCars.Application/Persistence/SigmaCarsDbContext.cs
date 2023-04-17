using Microsoft.EntityFrameworkCore;
using SigmaCars.Domain.Models;

namespace SigmaCars.Application.Persistence;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: use reflection or source generator to generate this code
        modelBuilder.Entity<Car>(e =>
        {
            e.ToTable("cars");
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.CarModelId).HasColumnName("car_model_id");
            e.Property(x => x.DepartmentId).HasColumnName("department_id");
            e.Property(x => x.RegistrationNumber).HasColumnName("registration_number");
            e.Property(x => x.Vin).HasColumnName("vin");
        });
        
        modelBuilder.Entity<CarModel>(e =>
        {
            e.ToTable("car_models");
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.Make).HasColumnName("make");
            e.Property(x => x.Model).HasColumnName("model");
            e.Property(x => x.ProductionYear).HasColumnName("production_year");
            e.Property(x => x.Color).HasColumnName("color");
            e.Property(x => x.PricePerDay).HasColumnName("price_per_day");
            e.Property(x => x.SeatCount).HasColumnName("seat_count");
        });
        
        modelBuilder.Entity<Department>(e =>
        {
            e.ToTable("departments");
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.CountryCode).HasColumnName("country_code");
            e.Property(x => x.City).HasColumnName("city");
            e.Property(x => x.Address).HasColumnName("address");
        });
        
        modelBuilder.Entity<Rental>(e =>
        {
            e.ToTable("rentals");
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.CarId).HasColumnName("car_id");
            e.Property(x => x.CustomerId).HasColumnName("customer_id");
            e.Property(x => x.StartDate).HasColumnName("start_date");
            e.Property(x => x.EndDate).HasColumnName("end_date");
        });
        
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.Property(x => x.Id).HasColumnName("id");
            e.Property(x => x.FirstName).HasColumnName("first_name");
            e.Property(x => x.LastName).HasColumnName("last_name");
            e.Property(x => x.Email).HasColumnName("email");
            e.Property(x => x.PasswordHash).HasColumnName("password_hash");
            e.Property(x => x.Role).HasColumnName("role");
        });
    }
}