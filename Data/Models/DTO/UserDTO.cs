using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models.DTO;

public class UserDTO
{
    [Required]
    public String FullName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public Gender gender { get; set; }
    [Required]
    public String Phone { get; set; }
    [Required]
    public String Email { get; set; }
    [Required]
    public Guid Address { get; set; }
    [Required]
    public Rating Rating { get; set; }
    public Order Order { get; set; }
    
}