using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class Hierarchy
{
    [Key]
    public Guid Id { get; set; }
    public int ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    public int ParentObjId { get; set; }
    public bool IsActive { get; set; }
    public House house { get; set; }
    public SearchAddressModel SearchAddressModel { get; set; }
}