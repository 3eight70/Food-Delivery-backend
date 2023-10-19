using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class DishInCart
{
    [Key]
    public Guid Id { get; set; }
    public int Count { get; set; }
    [ForeignKey("UserId")]
    public User user { get; set; }
    [ForeignKey("DishId")]
    public Dish dish { get; set; }
}