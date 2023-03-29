namespace SigmaCars.Domain;

public class Rental
{
    public int Id { get; set; }
    
    public int CarId { get; set; }
    
    public int CustomerId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
}