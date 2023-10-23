using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IDishService
{
    Task<DishPagedListDTO> GetDishes(Category[] categories, DishSorting sorting, bool vegetarian, int page);
    DishDTO GetDish(Guid id);
    bool CheckIfUserCanRateDish(string token, Guid dishId);
    Task<ActionResult> SetRating(string token, Guid id, Int32 ratingScore);
}