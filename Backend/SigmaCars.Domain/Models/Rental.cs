namespace SigmaCars.Domain.Models;

public class Rental
{
    public Rental(int carId, int userId, DateTime startDate, DateTime endDate, int id = 0)
    {
        Id = id;
        CarId = carId;
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public int Id { get; set; }

    public int CarId { get; set; }

    public int UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Car Car { get; set; } = null!;

    public User User { get; set; } = null!;
}