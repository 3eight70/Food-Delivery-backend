using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/basket")]
public class BasketController
{
    [Authorize]
    [HttpGet]
    public ActionResult Get()         //Доделать ActionResult<>
    {
        return null;
    }

    [Authorize]
    [HttpPost]
    [Route("dish/{dishId}")]
    public ActionResult AddDish()
    {
        return null;
    }

    [Authorize]
    [HttpDelete]
    [Route("dish/{dishId}")]
    public ActionResult Decrease()
    {
        return null;
    }
}