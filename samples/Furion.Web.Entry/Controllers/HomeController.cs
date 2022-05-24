using Furion.SpecificationDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Furion.Web.Entry.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly IMemoryCache _cache;
    public HomeController(IMemoryCache cache)
    {
        _cache = cache;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult IM()
    {
        return View();
    }

    [HttpGet]
    public int CheckUrl()
    {
        if (_cache.Get<bool>("swagger_login"))
        {
            return 200;
        }
        else
        {
            return 401;
        }
    }

    [HttpPost]
    public int SubmitUrl(SpecificationAuth auth)
    {
        if (auth.UserName == "admin")
        {
            _cache.Set<bool>("swagger_login", true);
            return 200;
        }
        else
        {
            return 401;
        }
    }
}
