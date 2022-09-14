using Furion.UnifyResult;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Furion.Application;

public class TestUnifyProvider : IDynamicApiController
{
    public string DefaultUnify()
    {
        return "test";
    }

    [UnifyProvider]
    public string DefaultUnify2()
    {
        return "test";
    }

    [UnifyProvider("specially")]
    public string SpeciallyUnify()
    {
        return "特别";
    }
}

[UnifyModel(typeof(MyResult<>))]
public class SpeciallyResultProvider : IUnifyResultProvider
{
    public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
    {
        return new ContentResult() { Content = "异常啦" };
    }

    public IActionResult OnSucceeded(ActionExecutedContext context, object data)
    {
        return new ContentResult() { Content = "成功啦" };
    }

    public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
    {
        return new ContentResult() { Content = "失败啦" };
    }

    public async Task OnResponseStatusCodes(HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings)
    {
        await Task.CompletedTask;
    }
}

public class MyResult<T>
{
    /// <summary>
    /// 数据
    /// </summary>
    public T Data { get; set; }
}