using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Furion.Web.Entry.Controllers;

/// <summary>
/// 控制器层注释
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TestApiController : ControllerBase
{
    [HttpGet, NonUnify]
    public string TestApi([MinLength(3)] string name)
    {
        return "Furion " + name;
    }
}
