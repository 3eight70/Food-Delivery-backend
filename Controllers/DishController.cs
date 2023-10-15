using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController
{
    [HttpGet]
    public ActionResult<Dish> GetList()
    {
        return null;
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Dish> GetInfo()
    {
        return null;
    }

    [HttpGet]
    [Route("{id}/rating/check")]
    public ActionResult<Dish> CheckAuth()
    {
        return null;
    }

    [HttpPost]
    [Route("{id}/rating")]
    public ActionResult<Dish> SetRating()
    {
        return null;
    }
}