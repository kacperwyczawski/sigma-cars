﻿using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Domain;

public class CarModel
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Make { get; set; }
    
    [MaxLength(50)]
    public string Model { get; set; }
    
    public int Year { get; set; }
    
    [MaxLength(50)]
    public string Color { get; set; }
    
    public float PricePerDay { get; set; }
}