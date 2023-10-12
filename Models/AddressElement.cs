using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class AddressElement
{
    [Key]
    public Guid Id { get; set; }
    public int ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    public String Name { get; set; }
    public String TypeName { get; set; }
    public int Level { get; set; }
    public bool IsActive { get; set; }
}