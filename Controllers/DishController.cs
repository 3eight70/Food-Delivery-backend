using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;
using webNET_Hits_backend_aspnet_project_1.Services;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController: ControllerBase
{
    private IDishService dishService;

    public DishController(IDishService _dishService)
    {
        dishService = _dishService;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetList([FromQuery]Category[] categories, DishSorting sorting, bool vegetarian = false, int page = 1)
    {
        try
        {
            var dishes = await dishService.GetDishes(categories, sorting, vegetarian, page);
            return Ok(dishes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong");
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetInfo(Guid id)
    {
        try
        {
            var dish = dishService.GetDish(id);
            return Ok(dish);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, new
            {
                status = "error",
                message = $"Dish with id={ex.Message} doesn't exist in database"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong");
        }
    }
    [Authorize]
    [HttpGet]
    [Route("{id}/rating/check")]
    public ActionResult<bool> CheckAuth(Guid id)
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return dishService.CheckIfUserCanRateDish(token, id);
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

    [Authorize]
    [HttpPost]
    [Route("{id}/rating")]
    public async Task<ActionResult> SetRating(Guid id, Int32 ratingScore)
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);
        
        if (ratingScore < 0 || ratingScore > 10)
        {
            return StatusCode(400, "Rating score must be between 0 and 10");
        }

        try
        {
            return await dishService.SetRating(token, id, ratingScore);
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