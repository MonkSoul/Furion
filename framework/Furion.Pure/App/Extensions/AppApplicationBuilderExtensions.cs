// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 应用中间件拓展类（由框架内部调用）
/// </summary>
[SuppressSniffer]
public static class AppApplicationBuilderExtensions
{
    /// <summary>
    /// 注入基础中间件（带Swagger）
    /// </summary>
    /// <param name="app"></param>
    /// <param name="routePrefix">空字符串将为首页</param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInject(this IApplicationBuilder app, string routePrefix = default, Action<UseInjectOptions> configure = null)
    {
        // 载入中间件配置选项
        var configureOptions = new UseInjectOptions();
        configure?.Invoke(configureOptions);

        app.UseSpecificationDocuments(routePrefix, UseInjectOptions.SwaggerConfigure, UseInjectOptions.SwaggerUIConfigure);

        return app;
    }

    /// <summary>
    /// 注入基础中间件（带Swagger）
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInject(this IApplicationBuilder app, Action<UseInjectOptions> configure)
    {
        return app.UseInject(default, configure: configure);
    }

    /// <summary>
    /// 注入基础中间件
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInjectBase(this IApplicationBuilder app)
    {
        return app;
    }

    /// <summary>
    /// 解决 .NET6 WebApplication 模式下二级虚拟目录错误问题
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder MapRouteControllers(this IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }

    /// <summary>
    /// 启用 Body 重复读功能
    /// </summary>
    /// <remarks>须在 app.UseRouting() 之前注册</remarks>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder EnableBuffering(this IApplicationBuilder app)
    {
        return app.Use(next => context =>
        {
            context.Request.EnableBuffering();
            return next(context);
        });
    }

    /// <summary>
    /// 添加应用中间件
    /// </summary>
    /// <param name="app">应用构建器</param>
    /// <param name="configure">应用配置</param>
    /// <returns>应用构建器</returns>
    internal static IApplicationBuilder UseApp(this IApplicationBuilder app, Action<IApplicationBuilder> configure = null)
    {
        // 调用自定义服务
        configure?.Invoke(app);
        return app;
    }
}