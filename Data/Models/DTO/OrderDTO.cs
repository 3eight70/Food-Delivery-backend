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
    public int Price { get; set; }
    [Required]
    private List<DishInCart> DishesInCart { get; set; }
}