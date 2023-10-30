using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class UserRegisterModel : IValidatableObject
{
    [Required]
    public String FullName { get; set; }
    [Required]
    [RegularExpression("^(?=.*\\d).{6,}$", ErrorMessage = "Password must be at least 6 letters and have at least 1 digit")]
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
    [Required]
    public Gender gender { get; set; }
    [RegularExpression("^\\+7 \\(\\d{3}\\) \\d{3}-\\d{2}-\\d{2}$", ErrorMessage = "Invalid phone number")]
    public String? Phone { get; set; }
    [Required]
    [RegularExpression("[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+", ErrorMessage = "Invalid email address")]
    public String Email { get; set; }
    public Guid? Address { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        if (BirthDate < new DateTime(1940, 01, 01) || BirthDate > DateTime.UtcNow)
        {
            errors.Add(new ValidationResult("BirthDate must be later than 1940.01.01 and earlier than now"));
        }

        return errors;
    }
}