// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Mvc.Filters;

namespace Furion.FriendlyException;

/// <summary>
/// 全局异常处理
/// </summary>
public interface IGlobalExceptionHandler
{
    /// <summary>
    /// 异常拦截
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task OnExceptionAsync(ExceptionContext context);
}