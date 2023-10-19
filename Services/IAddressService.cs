using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IAddressService
{
    SearchAddressModel[] Search(Int64 parentObjectId, string query);
}