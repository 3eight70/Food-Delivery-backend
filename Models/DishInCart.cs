namespace webNET_Hits_backend_aspnet_project_1.Models;

public class DishInCart
{
    public int Count { get; set; }
    public Order order { get; set; }
}