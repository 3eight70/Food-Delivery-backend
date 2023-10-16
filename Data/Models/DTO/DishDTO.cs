using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models.DTO;

public class DishDTO
{
    [Required]
    public String Name { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public String Photo { get; set; }
    [Required]
    public bool IsVegetarian { get; set; }
    public Rating Rating;
    [Required]
    public Category Category { get; set; }
}