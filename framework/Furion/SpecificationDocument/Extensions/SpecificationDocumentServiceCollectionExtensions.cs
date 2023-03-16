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
using Furion.SpecificationDocument;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 规范化接口服务拓展类
/// </summary>
[SuppressSniffer]
public static class SpecificationDocumentServiceCollectionExtensions
{
    /// <summary>
    /// 添加规范化文档服务
    /// </summary>
    /// <param name="mvcBuilder">Mvc 构建器</param>
    /// <param name="configure">自定义配置</param>
    /// <returns>服务集合</returns>
    public static IMvcBuilder AddSpecificationDocuments(this IMvcBuilder mvcBuilder, Action<SwaggerGenOptions> configure = default)
    {
        mvcBuilder.Services.AddSpecificationDocuments(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加规范化文档服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configure">自定义配置</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddSpecificationDocuments(this IServiceCollection services, Action<SwaggerGenOptions> configure = default)
    {
        // 判断是否启用规范化文档
        if (App.Settings.InjectSpecificationDocument != true) return services;

        // 添加配置
        services.AddConfigurableOptions<SpecificationDocumentSettingsOptions>();

#if !NET5_0
        services.AddEndpointsApiExplorer();
#endif

        // 添加Swagger生成器服务
        services.AddSwaggerGen(options => SpecificationDocumentBuilder.BuildGen(options, configure));

        // 添加 MiniProfiler 服务
        AddMiniProfiler(services);

        return services;
    }

    /// <summary>
    /// 添加 MiniProfiler 配置
    /// </summary>
    /// <param name="services"></param>
    private static void AddMiniProfiler(IServiceCollection services)
    {
        // 注册MiniProfiler 组件
        if (App.Settings.InjectMiniProfiler != true) return;

        services.AddMiniProfiler(options =>
        {
            // 减少非 Swagger 页面请求监听问题
            options.ShouldProfile = (req) =>
            {
                if (!req.Headers.ContainsKey("request-from")) return false;
                return true;
            };

            options.RouteBasePath = "/index-mini-profiler";
            options.EnableMvcFilterProfiling = false;
            options.EnableMvcViewProfiling = false;
        }).AddRelationalDiagnosticListener();
    }
}