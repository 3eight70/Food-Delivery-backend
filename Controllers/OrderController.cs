using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController
{
    [HttpGet]
    [Route("{id}")]
    public ActionResult<OrderDTO> GetInfo()
    {
        return null;
    }
    [HttpGet]
    public ActionResult<OrderDTO> GetOrders()
    {
        return null;
    }

    [HttpPost]
    public ActionResult<OrderDTO> CreateOrder()
    {
        return null;
    }

    [HttpPost]
    [Route("{id}/status")]
    public ActionResult<OrderDTO> Confirm()
    {
        return null;
    }
}