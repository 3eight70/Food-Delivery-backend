using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;
using webNET_Hits_backend_aspnet_project_1.Services;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ITokenService _tokenService;

    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger, ITokenService tokenService)
    {
        _orderService = orderService;
        _tokenService = tokenService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public ActionResult<OrderDTO> GetInfo(Guid id)
    {
        string? token = _tokenService.GetToken(Request.Headers["Authorization"].ToString());

        try
        {
            return _orderService.GetInfo(token, id);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new StatusResponse
            {
                Status = "Error",
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured with such id: {id}");

            return StatusCode(500, new StatusResponse
            {
                Status = "Error",
                Message = "Something went wrong"
            });
        }
    }

    [Authorize]
    [HttpGet]
    public ActionResult<OrderInfoDTO[]> GetOrders()
    {
        string? token = _tokenService.GetToken(Request.Headers["Authorization"].ToString());

        try
        {
            return _orderService.GetList(token);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new StatusResponse
            {
                Status = "Error",
                Message = "Something went wrong"
            });
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateOrder(OrderCreateDTO order)
    {
        string? token = _tokenService.GetToken(Request.Headers["Authorization"].ToString());

        try
        {
            return await _orderService.CreateOrder(order, token);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new StatusResponse
            {
                Status = "Error",
                Message = ex.Message
            });
        }
        catch (InvalidDataException ex)
        {
            return StatusCode(404, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured with such parameters: {order.AddressId}, {order.DeliveryTime}");

            return StatusCode(500, new StatusResponse
            {
                Status = "Error",
                Message = "Something went wrong"
            });
        }
    }

    [Authorize]
    [HttpPost]
    [Route("{id}/status")]
    public async Task<ActionResult> Confirm(Guid id)
    {
        string? token = _tokenService.GetToken(Request.Headers["Authorization"].ToString());

        try
        {
            return await _orderService.ConfirmOrder(token, id);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new StatusResponse
            {
                Status = "Error",
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured with such id: {id}");

            return StatusCode(500, new StatusResponse
            {
                Status = "Error",
                Message = "Something went wrong"
            });
        }
    }
}