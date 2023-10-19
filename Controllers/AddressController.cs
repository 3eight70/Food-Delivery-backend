using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Services;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/address")]
public class AddressController: ControllerBase
{
    private IAddressService addressService;

    public AddressController(IAddressService _addressService)
    {
        addressService = _addressService;
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
            return StatusCode(500, "Something went wrong");
        }
    }

    [HttpGet]
    [Route("getaddresschain")]
    public ActionResult<SearchAddressModel> GetChain()
    {
        return null;
    }
}