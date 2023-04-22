using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Domain.Models;

public class User
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }
    
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }
    
    [MaxLength(100)]
    public string PasswordHash { get; set; }

    [MaxLength(50)]
    public string Role { get; set; }
    
    public List<Rental> Rentals { get; set; }
}