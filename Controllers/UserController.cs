using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;
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
    public ActionResult<UserDTO> Get()
    {
        return userService.GetUserProfile();
    }

    [HttpPost]
    [Route("logout")]
    public ActionResult<UserDTO> Logout()
    {
        return null;
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Post(UserDTO model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(401, "User model is invalid.");
        }

        try
        {
            await userService.Add(model);
            return Ok(userService.RegisterUser());
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong with user model");
        }
        
    }
    
}