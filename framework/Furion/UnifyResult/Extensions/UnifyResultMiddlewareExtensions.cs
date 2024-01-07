// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.UnifyResult;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 状态码中间件拓展
/// </summary>
[SuppressSniffer]
public static class UnifyResultMiddlewareExtensions
{
    /// <summary>
    /// 添加状态码拦截中间件
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseUnifyResultStatusCodes(this IApplicationBuilder builder)
    {
        // 注册中间件
        builder.UseMiddleware<UnifyResultStatusCodesMiddleware>();

        return builder;
    }
}