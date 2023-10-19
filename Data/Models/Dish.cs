using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class Dish
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public String Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public String Photo { get; set; }
    [Required]
    public bool IsVegetarian { get; set; }
    public Rating Rating { get; set; }
    [Required]
    public Category Category { get; set; }

    public Dish(Guid id, string name, int price, string description, string photo, bool isVegetarian, Category category)
    {
        Id = id;
        Name = name;
        Price = price;
        Photo = photo;
        Description = description;
        IsVegetarian = isVegetarian;
        Category = category;
    }
}