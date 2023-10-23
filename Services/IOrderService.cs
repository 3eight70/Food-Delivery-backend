using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Services;

public interface IOrderService
{
    OrderInfoDTO[] GetList(string token);
    Task<ActionResult> CreateOrder(OrderCreateDTO order, string token);
}