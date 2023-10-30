using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models.DTO;

public class UserDTO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public String FullName { get; set; }
    public DateTime BirthDate { get; set; }
    [Required]
    public Gender gender { get; set; }
    public String Phone { get; set; }
    [Required]
    public String Email { get; set; }
    public Guid Address { get; set; }
}