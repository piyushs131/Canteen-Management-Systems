using DBMS_Project.Enums;

namespace DBMS_Project.Models;

public class Category
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public double Price { get; set; }
    
    public string Image { get; set; }
    
    public string Type { get; set; }
    
    public string Size { get; set; }
    
    public bool IsAvailable { get; set; }
    

   
    public PlaceType Place { get; set; }
}