using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;
using webNET_Hits_backend_aspnet_project_1.Services;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController: ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public ActionResult<OrderDTO> GetInfo(Guid id)
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return _orderService.GetInfo(token, id);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Something went wrong");
        }
    }

    [Authorize]
    [HttpGet]
    public ActionResult<OrderInfoDTO[]> GetOrders()
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return _orderService.GetList(token);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong");
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateOrder(OrderCreateDTO order)
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return await _orderService.CreateOrder(order, token);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (InvalidDataException ex)
        {
            return StatusCode(404, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong");
        }
    }

    [Authorize]
    [HttpPost]
    [Route("{id}/status")]
    public async Task<ActionResult> Confirm(Guid id)
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return await _orderService.ConfirmOrder(token, id);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Something went wrong");
        }
    }
}