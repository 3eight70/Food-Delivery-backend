using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IUserService
{
    UserDTO GetUserProfile(string token);
    Task<ActionResult> RegisterUser(UserRegisterModel model);

    Task<ActionResult> LogoutUser(string token);

    string LoginUser(LoginCredentials userData);

    Task<ActionResult> EditUserProfile(UserEditModel model, string token);
}