using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class Dish
{
    [Key]
    public Guid Id { get; set; }
    public String Name { get; set; }
    public int Price { get; set; }
    public String Photo { get; set; }
    public bool IsVegetarian { get; set; }
    public Rating rating;
    public Category Category { get; set; }
}