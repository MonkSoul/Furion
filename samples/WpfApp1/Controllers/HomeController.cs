using Furion.FriendlyException;
using Furion.Localization;
using Furion.SpecificationDocument;
using Furion.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Furion.Web.Entry.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        var test = L.GetString<SharedResource>(u => u.实时通信);
        return View();
    }

    public IActionResult IM()
    {
        return View();
    }

    [HttpPost, AllowAnonymous, NonUnify]
    public int CheckUrl()
    {
        return 401;
    }

    [HttpPost, AllowAnonymous, NonUnify]
    public int SubmitUrl([FromForm] SpecificationAuth auth)
    {
        // 读取配置信息
        var userName = App.Configuration["SpecificationDocumentSettings:LoginInfo:UserName"];
        var password = App.Configuration["SpecificationDocumentSettings:LoginInfo:Password"];

        // 判断用户名密码
        if (auth.UserName == userName && auth.Password == password)
        {
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