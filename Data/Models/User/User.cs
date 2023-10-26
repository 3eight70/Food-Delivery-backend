using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public String FullName { get; set; }
    public DateTime BirthDate { get; set; }
    [Required]
    public Gender gender { get; set; } 
    [RegularExpression("^\\+7 \\(\\d{3}\\) \\d{3}-\\d{2}-\\d{2}$", ErrorMessage = "Invalid phone number")]
    public String Phone { get; set; }
    [Required]
    [RegularExpression("[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+", ErrorMessage = "Invalid email address")]
    public String Email { get; set; }
    public Guid Address { get; set; }
    [Required]
    [RegularExpression("^(?=.*\\d).{6,}$", ErrorMessage = "Password must be at least 6 letters and have at least 1 digit")]
    public String Password { get; set; }

    public User(Guid id, string fullName, DateTime birthDate, Gender gender, string phone, string email, string password)
    {
        Id = id;
        FullName = fullName;
        BirthDate = birthDate;
        this.gender = gender;
        Phone = phone;
        Email = email;
        Password = password;
    }
}