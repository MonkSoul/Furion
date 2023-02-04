// MIT License
//
// Copyright (c) 2020-present 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion.Schedule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
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

        // 实现路由重写，配置入口地址
        if (options.RequestPath != ScheduleUIMiddleware.REQUEST_PATH)
        {
            app.UseRewriter(new RewriteOptions()
                .AddRedirect($"{options.RequestPath[1..]}", $"{ScheduleUIMiddleware.REQUEST_PATH[1..]}")
                .AddRedirect($"{options.RequestPath[1..]}/(.*)", $"{ScheduleUIMiddleware.REQUEST_PATH[1..]}/$1"));
        }

        // 注册 Schedule 中间件
        app.UseMiddleware<ScheduleUIMiddleware>();

        // 获取当前类型所在程序集
        var currentAssembly = typeof(ScheduleUIExtensions).Assembly;

        // 注册嵌入式文件服务器
        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new EmbeddedFileProvider(currentAssembly, $"{currentAssembly.GetName().Name}.Schedule.Dashboard.frontend"),
            RequestPath = ScheduleUIMiddleware.REQUEST_PATH,  // 内部固定
            EnableDirectoryBrowsing = options.EnableDirectoryBrowsing
        });

        return app;
    }
}