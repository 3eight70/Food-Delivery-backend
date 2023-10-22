using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IBasketService
{
    DishBasketDTO[] GetCart(string token);
    Task<ActionResult> AddDish(string token, Guid dishId);
}