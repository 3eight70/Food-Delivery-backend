using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class UserEditModel
{
    [Required]
    public String FullName { get; set; }
    public DateTime BirthDate { get; set; }
    [Required]
    public Gender gender { get; set; }
    [RegularExpression("^\\+7 \\(\\d{3}\\) \\d{3}-\\d{2}-\\d{2}$", ErrorMessage = "Invalid phone number")]
    public String Phone { get; set; }
    public Guid Address { get; set; }
}