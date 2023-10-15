using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
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

    public User(Guid id, String fullName, DateTime birthDate, Gender gender, String phone, String email)
    {
        Id = id;
        FullName = fullName;
        BirthDate = birthDate;
        this.gender = gender;
        Phone = phone;
        Email = email;
    }
}