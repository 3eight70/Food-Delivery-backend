using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;

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
    public String Phone { get; set; }
    [Required]
    public String Email { get; set; }
    public Guid Address { get; set; }
    [Required]
    public string Password { get; set; }

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