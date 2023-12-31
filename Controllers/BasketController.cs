﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Services;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/basket")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    private readonly ILogger<BasketController> _logger;

    public BasketController(IBasketService basketService, ILogger<BasketController> logger)
    {
        _basketService = basketService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    public ActionResult Get()
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return Ok(_basketService.GetCart(token));
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
    [Route("dish/{dishId}")]
    public async Task<ActionResult> AddDish(Guid dishId)
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return await _basketService.AddDish(token, dishId);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured with such id: {dishId}");

            return StatusCode(500, new StatusResponse
            {
                Status = "Error",
                Message = "Something went wrong"
            });
        }
    }

    [Authorize]
    [HttpDelete]
    [Route("dish/{dishId}")]
    public async Task<ActionResult> Decrease(Guid dishId, bool increase = false)
    {
        var token = Request.Headers["Authorization"].ToString();
        token = token.Substring("Bearer ".Length);

        try
        {
            return await _basketService.DecreaseDish(token, dishId, increase);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured with such id and bool param: {dishId}, {increase}");

            return StatusCode(500, new StatusResponse
            {
                Status = "Error",
                Message = "Something went wrong"
            });
        }
    }
}