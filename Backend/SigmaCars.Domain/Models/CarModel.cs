using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Domain.Models;

public class CarModel
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Make { get; set; }
    
    [MaxLength(50)]
    public string Model { get; set; }
    
    public int ProductionYear { get; set; }
    
    [MaxLength(50)]
    public string Color { get; set; }
    
    [Range(0, float.MaxValue)]
    public float PricePerDay { get; set; }
    
    [Range(0, int.MaxValue)]
    public int SeatCount { get; set; }

    public override string ToString()
    {
        return $"{nameof(CarModel)}: {Color} {Make} {Model} from {ProductionYear} with {SeatCount} seats and price {PricePerDay} per day";
    }
}