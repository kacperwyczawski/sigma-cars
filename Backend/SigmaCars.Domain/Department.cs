namespace SigmaCars.Domain;

public class Department
{
    public int Id { get; set; }
    
    /// <summary>
    /// Two letter ISO country code
    /// </summary>
    public string CountryCode { get; set; }
    
    public string City { get; set; }
    
    public string Address { get; set; }
}