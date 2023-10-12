using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public String FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender gender { get; set; }
    public String Phone { get; set; }
    public String Email { get; set; }
    public Guid Address { get; set; }
    public Rating rating;
    public Order order;
}