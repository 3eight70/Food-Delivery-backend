using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models.DTO;

public class DishBasketDTO
{
    public Guid Id { get; set; }
    [Required]
    public String Name { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public double TotalPrice { get; set; }
    [Required] 
    public int Amount { get; set; }
    public String? Image { get; set; }
}