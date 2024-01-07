// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Furion.UnifyResult;

/// <summary>
/// 状态码中间件
/// </summary>
[SuppressSniffer]
public class UnifyResultStatusCodesMiddleware
{
    /// <summary>
    /// 请求委托
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    public UnifyResultStatusCodesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// 中间件执行方法
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        // 只有请求错误（短路状态码）和非 WebSocket 才支持规范化处理
        if (context.IsWebSocketRequest()
            || context.Response.StatusCode < 400
            || context.Response.StatusCode == 404) return;

        // 处理规范化结果
        if (!UnifyContext.CheckStatusCodeNonUnify(context, out var unifyResult))
        {
            // 解决刷新 Token 时间和 Token 时间相近问题
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized
                && context.Response.Headers.ContainsKey("access-token")
                && context.Response.Headers.ContainsKey("x-access-token"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
            }

            // 如果 Response 已经完成输出，则禁止写入
            if (context.Response.HasStarted) return;
            await unifyResult.OnResponseStatusCodes(context, context.Response.StatusCode, context.RequestServices.GetService<IOptions<UnifyResultSettingsOptions>>()?.Value);
        }
    }
}