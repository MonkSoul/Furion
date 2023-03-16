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

using Furion;
using Furion.CorsAccessor;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 跨域访问服务拓展类
/// </summary>
[SuppressSniffer]
public static class CorsAccessorServiceCollectionExtensions
{
    /// <summary>
    /// 配置跨域
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="corsOptionsHandler"></param>
    /// <param name="corsPolicyBuilderHandler"></param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddCorsAccessor(this IServiceCollection services, Action<CorsOptions> corsOptionsHandler = default, Action<CorsPolicyBuilder> corsPolicyBuilderHandler = default)
    {
        // 添加跨域配置选项
        services.AddConfigurableOptions<CorsAccessorSettingsOptions>();

        // 获取选项
        var corsAccessorSettings = App.GetConfig<CorsAccessorSettingsOptions>("CorsAccessorSettings", true);

        // 添加跨域服务
        services.AddCors(options =>
        {
            // 添加策略跨域
            options.AddPolicy(corsAccessorSettings.PolicyName, builder =>
            {
                // 设置跨域策略
                Penetrates.SetCorsPolicy(builder, corsAccessorSettings);

                // 添加自定义配置
                corsPolicyBuilderHandler?.Invoke(builder);
            });

            // 添加自定义配置
            corsOptionsHandler?.Invoke(options);
        });

        return services;
    }
}