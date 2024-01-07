// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.CorsAccessor;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 跨域中间件拓展
/// </summary>
[SuppressSniffer]
public static class CorsAccessorApplicationBuilderExtensions
{
    /// <summary>
    /// 添加跨域中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="corsPolicyBuilderHandler"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCorsAccessor(this IApplicationBuilder app, Action<CorsPolicyBuilder> corsPolicyBuilderHandler = default)
    {
        // 获取选项
        var corsAccessorSettings = app.ApplicationServices.GetService<IOptions<CorsAccessorSettingsOptions>>().Value;

        // 判断是否启用 SignalR 跨域支持
        if (corsAccessorSettings.SignalRSupport == false)
        {
            // 配置跨域中间件
            _ = corsPolicyBuilderHandler == null
                   ? app.UseCors(corsAccessorSettings.PolicyName)
                   : app.UseCors(corsPolicyBuilderHandler);
        }
        else
        {
            // 配置跨域中间件
            app.UseCors(builder =>
            {
                // 设置跨域策略
                Penetrates.SetCorsPolicy(builder, corsAccessorSettings, true);

                // 添加自定义配置
                corsPolicyBuilderHandler?.Invoke(builder);
            });
        }

        return app;
    }
}