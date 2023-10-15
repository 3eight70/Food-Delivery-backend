using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class House
{
    [Key]
    public Guid Id { get; set; }
    public int ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    public String HouseNum { get; set; }
    public String AddNum1 { get; set; }
    public String AddNum2 { get; set; }
    public String AddType1 { get; set; }
    public String AddType2 { get; set; }
    public bool IsActive { get; set; }
}