namespace SigmaCars.Domain.Models;

public class CarModel
{
    public int Id { get; set; }
    
    public string Make { get; set; }
    
    public string Model { get; set; }
    
    public int ProductionYear { get; set; }
    
    public string Color { get; set; }
    
    public float PricePerDay { get; set; }
    
    public int SeatCount { get; set; }

    public override string ToString()
    {
        return $"{nameof(CarModel)}: {Color} {Make} {Model} from {ProductionYear} with {SeatCount} seats and price {PricePerDay} per day";
    }
}