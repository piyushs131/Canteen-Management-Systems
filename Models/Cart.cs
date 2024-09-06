using System.ComponentModel.DataAnnotations.Schema;
using DBMS_Project.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace DBMS_Project.Models;

public class Cart
{
    public int Id { get; set; }
    [ForeignKey("User")]
    public string? UserId { get; set; } // Foreign key to AspNetUsers table

    public IdentityUser User { get; set; } // Navigation property to ApplicationUser
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public double? Price { get; set; }
    
    public string? Image { get; set; }
    
    public string? Type { get; set; }
    
    public string? Size { get; set; }
    
    public bool IsAvailable { get; set; }
    
    

   
    public PlaceType Place { get; set; }

    public int quantity { get; set; }
}