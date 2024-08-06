// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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

        // 如果入口地址为空则不启动看板
        if (string.IsNullOrWhiteSpace(options.RequestPath)) return app;

        // 修复无效的入口地址
        options.RequestPath = $"/{options.RequestPath.TrimStart('/').TrimEnd('/')}";

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