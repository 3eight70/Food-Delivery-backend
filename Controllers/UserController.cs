using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[Route("api/account/")]
[ApiController]
public class UserController: ControllerBase
{
    [HttpGet]
    [Route("profile")]
    public ActionResult<User> Get()
    {
        User user = new User(Guid.NewGuid(), "Blablabla", DateTime.Now, Gender.Male, "+79539231131", "gbhfns@gmail.com", "fgdbfdbfd");
        return user;
    }

    [HttpPost]
    [Route("logout")]
    public ActionResult<User> Post()
    {
        return null;
    }
    
}