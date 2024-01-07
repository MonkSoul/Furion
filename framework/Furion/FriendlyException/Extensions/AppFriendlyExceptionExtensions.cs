// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Http;

namespace Furion.FriendlyException;

/// <summary>
/// 异常拓展
/// </summary>
[SuppressSniffer]
public static class AppFriendlyExceptionExtensions
{
    /// <summary>
    /// 设置异常状态码
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public static AppFriendlyException StatusCode(this AppFriendlyException exception, int statusCode = StatusCodes.Status500InternalServerError)
    {
        exception.StatusCode = statusCode;
        return exception;
    }

    /// <summary>
    /// 设置额外数据
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static AppFriendlyException WithData(this AppFriendlyException exception, object data)
    {
        exception.Data = data;
        return exception;
    }
}