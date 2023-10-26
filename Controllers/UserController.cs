using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<ActionResult> Logout()
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);
        
        return await userService.LogoutUser(token);
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
            return Ok(new
            {
                access_token = response
            });
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

        try
        {
            return await userService.RegisterUser(model);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
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
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return userService.GetUserProfile(token);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong");
        }
    }

    [Authorize]
    [HttpPut]
    [Route("profile")]
    public async Task<ActionResult> Edit(UserEditModel model)
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return await userService.EditUserProfile(model, token);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong");
        }
    }
}