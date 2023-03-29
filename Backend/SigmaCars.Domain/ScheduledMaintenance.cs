namespace SigmaCars.Domain;

public class ScheduledMaintenance
{
    public int Id { get; set; }
    
    public int CarId { get; set; }
    
    public int EmployeeId { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Reason { get; set; }
}