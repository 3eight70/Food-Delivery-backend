using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    public DateTime DeliveryTime { get; set; }
    public DateTime OrderTime { get; set; }
    public Status status { get; set; }
    public int Price { get; set; }
    private List<DishInCart> DishesInCart { get; set; }
}