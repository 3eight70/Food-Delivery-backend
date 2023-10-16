using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }
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