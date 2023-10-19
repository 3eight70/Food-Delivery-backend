using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class ActiveToken
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey("UserId")]
    public User user { get; set; }           //Узнать насколько это вообще корректно, т.к, как я понял user тупо принимает значение Guid, тогда как потом обращаться
    [Required]
    public String token { get; set; }
    [Required]
    public DateTime ExpirationDate { get; set; }
    public ActiveToken(Guid _id, User _user, string _token, DateTime date)
    {
        Id = _id;
        user = _user;
        token = _token;
        ExpirationDate = date;
    }
    
    public ActiveToken(){}
}