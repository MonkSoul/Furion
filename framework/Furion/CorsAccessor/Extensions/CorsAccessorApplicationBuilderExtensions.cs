// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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