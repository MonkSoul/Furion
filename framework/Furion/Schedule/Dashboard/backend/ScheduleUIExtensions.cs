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

using Furion.Schedule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Schedule 模块 UI 中间件拓展
/// </summary>
[SuppressSniffer]
public static class ScheduleUIExtensions
{
    /// <summary>
    /// 添加 Schedule 模块 UI 中间件
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    /// <param name="configureAction">Schedule 模块 UI 配置选项委托</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseScheduleUI(this IApplicationBuilder app, Action<ScheduleUIOptions> configureAction = default)
    {
        var scheduleUIOptions = new ScheduleUIOptions();
        configureAction?.Invoke(scheduleUIOptions);

        return app.UseScheduleUI(scheduleUIOptions);
    }

    /// <summary>
    /// 添加 Schedule 模块 UI 中间件
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    /// <param name="options">Schedule 模块 UI 配置选项</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseScheduleUI(this IApplicationBuilder app, ScheduleUIOptions options)
    {
        // 判断是否配置了定时任务服务
        if (app.ApplicationServices.GetService<ISchedulerFactory>() == null) return app;

        // 初始化默认值
        options ??= new ScheduleUIOptions();

        // 生产环境关闭
        if (options.DisableOnProduction
            && app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsProduction()) return app;

        // 如果路由为空，或者不以 / 开头，或者以 / 结尾，不启动看板
        if (string.IsNullOrWhiteSpace(options.RequestPath) || !options.RequestPath.StartsWith("/") || options.RequestPath.EndsWith("/")) return app;

        // 验证看板刷新频次（毫秒），至少大于 300ms
        if (options.SyncRate < 300) throw new InvalidOperationException($"The sync rate cannot be less than 300ms, but the value is <{options.SyncRate}ms>.");

        // 注册 Schedule 中间件
        app.UseMiddleware<ScheduleUIMiddleware>(options);

        // 获取当前类型所在程序集
        var currentAssembly = typeof(ScheduleUIExtensions).Assembly;

        // 注册嵌入式文件服务器
        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new EmbeddedFileProvider(currentAssembly, $"{currentAssembly.GetName().Name}.Schedule.Dashboard.frontend"),
            RequestPath = options.RequestPath,
            EnableDirectoryBrowsing = options.EnableDirectoryBrowsing
        });

        return app;
    }
}