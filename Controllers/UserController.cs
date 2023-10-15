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
    
    [HttpPost]
    [Route("logout")]
    public ActionResult<User> Logout()
    {
        return null;
    }
    
    [HttpPost]
    [Route("login")]
    public ActionResult<User> Login()
    {
        return null;
    }
    
    [HttpPost]
    [Route("register")]
    public ActionResult<User> Register()
    {
        return userService.RegisterUser();
    }
    
    [HttpGet]
    [Route("profile")]
    public ActionResult<User> Get()
    {
        return userService.GetUserProfile();
    }

    [HttpPut]
    [Route("profile")]
    public ActionResult<User> Edit()
    {
        return null;
    }
}