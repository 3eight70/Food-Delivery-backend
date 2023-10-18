using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController: ControllerBase
{
    [HttpGet]
    [Route("{id}")]
    public ActionResult<Order> GetInfo()
    {
        return null;
    }
    [HttpGet]
    public ActionResult<Order> GetOrders()
    {
        return null;
    }

    [HttpPost]
    public ActionResult<Order> CreateOrder()
    {
        return null;
    }

    [HttpPost]
    [Route("{id}/status")]
    public ActionResult<Order> Confirm()
    {
        return null;
    }
}