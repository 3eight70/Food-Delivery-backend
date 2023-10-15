using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IUserService
{
    User RegisterUser();
    User GetUserProfile();
    Task Add(User model);
}