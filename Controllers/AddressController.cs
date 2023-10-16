using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_1.Models;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/address")]
public class AddressController
{
    [HttpGet]
    [Route("search")]
    public ActionResult<AddressElement> Search()
    {
        return null;
    }

    [HttpGet]
    [Route("getaddresschain")]
    public ActionResult<AddressElement> GetChain()
    {
        return null;
    }
}