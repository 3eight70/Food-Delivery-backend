using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class DishInCart
{
    [Key]
    public Guid Id { get; set; }
    public int Amount { get; set; }
    [ForeignKey("UserId")]
    public Guid userId { get; set; }
    [ForeignKey("DishId")]
    public Guid dishId { get; set; }
}