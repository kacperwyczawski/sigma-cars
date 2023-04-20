namespace SigmaCars.Domain.Models;

public class Car
{
    public int Id { get; set; }
    
    public int CarModelId { get; set; }
    
    public int DepartmentId { get; set; }
    
    public string RegistrationNumber { get; set; }
    
    public string Vin { get; set; }
}