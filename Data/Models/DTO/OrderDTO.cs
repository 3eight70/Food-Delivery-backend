using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models.DTO;

public class OrderDTO
{
    [Required]
    public DateTime DeliveryTime { get; set; }
    [Required]
    public DateTime OrderTime { get; set; }
    [Required]
    public Status status { get; set; }
    [Required]
    public double Price { get; set; }
    public List<DishBasketDTO> DishesInCart { get; set; }
}