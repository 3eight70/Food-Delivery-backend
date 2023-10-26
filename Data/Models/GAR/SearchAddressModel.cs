using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_1.Data.Models;

namespace webNET_Hits_backend_aspnet_project_1.Models;

public class SearchAddressModel
{
    public Int64 ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    public String? Text { get; set; }
    public GarAddressLevel? ObjectLevel { get; set; }
    public String? ObjectLevelText { get; set; }
}   