using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Services;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[Route("api/account/")]
[ApiController]
public class UserController: ControllerBase
{
    private IUserService userService;

    public UserController(IUserService user)
    {
        userService = user;
    }
    
    [HttpGet]
    [Route("profile")]
    public ActionResult<User> Get()
    {
        return userService.GetUserProfile();
    }

    [HttpPost]
    [Route("logout")]
    public ActionResult<User> Post()
    {
        return null;
    }
    
}