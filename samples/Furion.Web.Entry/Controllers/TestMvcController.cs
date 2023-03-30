using Microsoft.AspNetCore.Mvc;

namespace Furion.Web.Entry.Controllers;

//[Route("api/[controller]")]
public class TestMvcController : Controller
{
    //[HttpGet]
    [HttpGet("api/test")]
    public IActionResult Index()
    {
        return Content("ddd");
    }
}
