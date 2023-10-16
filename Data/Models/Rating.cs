using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class Rating
{
    [Key] 
    public Guid Id { get; set; }
    [Required]
    [ForeignKey("UserId")]
    public User user { get; set; }
    [Required]
    [ForeignKey("DishId")]
    public Dish dish { get; set; }
    public double Value { get; set; }
}