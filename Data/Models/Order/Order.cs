using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

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
    public double Price { get; set; }
    [Required]
    public List<DishBasketDTO> DishesInCart { get; set; }
    [ForeignKey("UserId")]
    public Guid userId { get; set; }
}