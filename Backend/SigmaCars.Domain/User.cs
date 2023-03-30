﻿using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Domain;

public class User
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }
    
    [MaxLength(100)]
    public string Email { get; set; }
    
    [MaxLength(100)]
    public string PasswordHash { get; set; }

    [MaxLength(50)]
    public UserRole Role { get; set; }
}