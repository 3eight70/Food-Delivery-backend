using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController: ControllerBase
{
    [HttpGet]
    public Task<ActionResult> GetList([BindRequired] Category[] categories, bool vegetarian, DishSorting sorting, int page)
    {
        try
        {

        }
        catch (Exception ex)
        {
            
        }
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