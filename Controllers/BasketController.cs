using Microsoft.AspNetCore.Mvc;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/basket")]
public class BasketController: ControllerBase
{
    [HttpGet]
    public ActionResult Get()         //Доделать ActionResult<>
    {
        return null;
    }

    [HttpPost]
    [Route("dish/{dishId}")]
    public ActionResult AddDish()
    {
        return null;
    }

    [HttpDelete]
    [Route("dish/{dishId}")]
    public ActionResult Decrease()
    {
        return null;
    }
}