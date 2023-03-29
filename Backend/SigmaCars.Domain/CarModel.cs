namespace SigmaCars.Domain;

public class CarModel
{
    public int Id { get; set; }
    
    public string Make { get; set; }
    
    public string Model { get; set; }
    
    public int Year { get; set; }
    
    public string Color { get; set; }
    
    public decimal PricePerDay { get; set; }
}