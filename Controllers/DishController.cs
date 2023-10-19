using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using webNET_Hits_backend_aspnet_project_1.Models;
using webNET_Hits_backend_aspnet_project_1.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_1.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController
{
    [HttpGet]
    public ActionResult<DishDTO> GetList()
    {
        return null;
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<DishDTO> GetInfo()
    {
        return null;
    }

    [Authorize]
    [HttpGet]
    [Route("{id}/rating/check")]
    public ActionResult<DishDTO> CheckAuth()
    {
        return null;
    }

    [Authorize]
    [HttpPost]
    [Route("{id}/rating")]
    public ActionResult<DishDTO> SetRating()
    {
        return null;
    }
}