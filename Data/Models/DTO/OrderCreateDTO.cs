using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models.DTO;

public class OrderCreateDTO
{
    [Required]
    public DateTime DeliveryTime { get; set; }
    [Required]
    public Guid AddressId { get; set; }
}