namespace SigmaCars.Domain.Models;

public class CarType
{
    public int Id { get; set; }
    
    public string Make { get; set; }
    
    public string Model { get; set; }
    
    public int ProductionYear { get; set; }
    
    public float PricePerDay { get; set; }
    
    public int SeatCount { get; set; }
    
    public byte[] Image { get; set; }

    public override string ToString()
    {
        return $"{nameof(CarType)}: {Make} {Model} from {ProductionYear} with {SeatCount} seats and price {PricePerDay} per day";
    }
    
    public List<Car> Cars { get; set; }
}