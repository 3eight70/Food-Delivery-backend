using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class ActiveToken
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("UserId")]
    public Guid userId { get; set; }           
    [Required]
    public String token { get; set; }
    public DateTime ExpirationDate { get; set; }
    public ActiveToken(Guid _id, Guid _userId, string _token, DateTime date)
    {
        Id = _id;
        userId = _userId;
        token = _token;
        ExpirationDate = date;
    }
    
    public ActiveToken(){}
}