using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Data;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Services;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/address")]
public class AddressController : ControllerBase
{
    private IAddressService addressService;

    private readonly ILogger<AddressController> _logger;

    public AddressController(IAddressService _addressService, ILogger<AddressController> logger)
    {
        addressService = _addressService;
        _logger = logger;
    }

    [HttpGet]
    [Route("search")]
    public async Task<ActionResult> Search(Int64 parentObjectId, string? query)
    {
        try
        {
            return Ok(addressService.Search(parentObjectId, query));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured with such params: {parentObjectId}, {query}");

            return StatusCode(500, new StatusResponse
            {
                Status = "Error",
                Message = "Something went wrong"
            });
        }
    }

    [HttpGet]
    [Route("getaddresschain")]
    public ActionResult<SearchAddressModel> GetChain(Guid objectGuid)
    {
        try
        {
            return Ok(addressService.SearchAddressChain(objectGuid));
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occured with such id: {objectGuid}");

            return StatusCode(500, new StatusResponse
            {
                Status = "Error",
                Message = "Something went wrong"
            });
        }
    }
}