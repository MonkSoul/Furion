using Furion.FriendlyException;
using Furion.SpecificationDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;

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

    [HttpPost]
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
    public int SubmitUrl([FromForm] SpecificationAuth auth)
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

    [NonUnify]
    public IActionResult TestValidate([Required, MinLength(3)] string data)
    {
        return Content("数据");
    }

    public IActionResult TestBad()
    {
        return new BadPageResult(500)
        {
            Title = "错误标题",
            Code = "详细错误消息",
            CodeLang = "语言",
            Description = "这里是一段错误描述",
        };
    }
}
