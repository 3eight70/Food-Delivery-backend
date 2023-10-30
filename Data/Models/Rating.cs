using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webNET_Hits_backend_aspnet_project_1.Models;

[Index(nameof(Rating.UserId), IsUnique = true)]
[Index(nameof(Rating.DishId), IsUnique = true)]

public class Rating
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    [ForeignKey("DishId")]
    public Guid DishId { get; set; }
    public double Value { get; set; }

    public Rating(Guid id, Guid userId, Guid dishId, double value)
    {
        Id = id;
        UserId = userId;
        DishId = dishId;
        Value = value;
    }
}