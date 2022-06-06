using FurionMvc.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furion.Web.Entry.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ISystemService _systemService;

    public HomeController(ISystemService systemService)
    {
        _systemService = systemService;
    }

    public IActionResult Index()
    {
        ViewBag.Description = _systemService.GetDescription();

        return View();
    }
}
