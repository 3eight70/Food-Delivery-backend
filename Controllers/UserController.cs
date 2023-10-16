using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize]
    [HttpPost]
    [Route("logout")]
    public ActionResult<UserDTO> Logout()
    {
        return null;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login(LoginCredentials model)
    {
        try
        {
            var response = userService.LoginUser(model);
            if (response == null)
            {
                return StatusCode(400, new
                {
                    status = "null",
                    message = "Login failed"
                });
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Invalid email or password");
        }
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Post(UserRegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return StatusCode(401, "User model is invalid.");
        }

        if (model.Phone != null && !IsValidPhoneNumber(model.Phone))
        {
            return StatusCode(400, "Invalid phone number");
        }

        try
        {
            await userService.RegisterUser(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong with user model");
        }
        
    }
    
   [Authorize]
    [HttpGet]
    [Route("profile")]
    public ActionResult<UserDTO> Get()
    {
        return userService.GetUserProfile();
    }

    [Authorize]
    [HttpPut]
    [Route("profile")]
    public ActionResult<UserEditModel> Edit(UserEditModel model)
    {
        return userService.EditUserProfile(model);
    }
    
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$");
    }
}