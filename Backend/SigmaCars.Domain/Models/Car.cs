namespace SigmaCars.Domain.Models;

public class Car
{
    public int Id { get; set; }
    
    public int carTypeId { get; set; }
    
    public int DepartmentId { get; set; }
    
    public string RegistrationNumber { get; set; }
    
    public string Vin { get; set; }
    
    public CarType CarType { get; set; }
    
    public Department Department { get; set; }
    
    public List<Rental> Rentals { get; set; }
}