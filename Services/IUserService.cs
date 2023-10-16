using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IUserService
{
    User RegisterUser();
    UserDTO GetUserProfile();
    Task Add(UserDTO model);
}