// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

        // 注册 Schedule 中间件
        app.UseMiddleware<ScheduleUIMiddleware>(new object[]
        {
            $"{options.RequestPath}/api"
        });

        // 注册嵌入式文件服务器
        app.UseFileServer(new FileServerOptions
        {
            FileProvider = new EmbeddedFileProvider(typeof(ScheduleUIExtensions).Assembly, $"{typeof(Scheduler).Namespace}.Dashboard.frontend"),
            RequestPath = options.RequestPath,
            EnableDirectoryBrowsing = options.EnableDirectoryBrowsing
        });

        return app;
    }
}