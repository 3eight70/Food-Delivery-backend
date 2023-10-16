using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IUserService
{
    UserDTO GetUserProfile();
    Task RegisterUser(UserRegisterModel model);

    IResult LoginUser(LoginCredentials userData);

    UserEditModel EditUserProfile(UserEditModel model);
}
