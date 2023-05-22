using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Domain.Models;

public class Department
{
    public int Id { get; set; }
    
    /// <summary>
    /// Two letter ISO 3166-1 alpha-2 country code
    /// </summary>
    [MaxLength(2)]
    [MinLength(2)]
    public string CountryCode { get; set; }
    
    [MaxLength(50)]
    public string City { get; set; }
    
    [MaxLength(100)]
    public string Address { get; set; }
    
    public List<Car> Cars { get; set; }
}