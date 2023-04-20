namespace SigmaCars.Domain.Models;

public class Rental
{
    public int Id { get; set; }
    
    public int CarId { get; set; }
    
    public int CustomerId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public Car Car { get; set; }
    
    public User User { get; set; }
}