using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class DishInCart
{
    [Key]
    public Guid Id { get; set; }
    public int Count { get; set; }
    public Order Order { get; set; }
}