// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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

#if !NET5_0
using Microsoft.AspNetCore.Builder;
#endif
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Serilog 日志拓展
/// </summary>
public static class SerilogHostingExtensions
{
    /// <summary>
    /// 添加默认日志拓展
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <param name="configAction"></param>
    /// <returns>IWebHostBuilder</returns>
#if !NET5_0
    [Obsolete("Prefer UseSerilog() on IHostBuilder")]
#endif
    public static IWebHostBuilder UseSerilogDefault(this IWebHostBuilder hostBuilder, Action<LoggerConfiguration> configAction = default)
    {
        // 判断是否是单文件环境
        var isSingleFileEnvironment = string.IsNullOrWhiteSpace(Assembly.GetEntryAssembly().Location);

        hostBuilder.UseSerilog((context, configuration) =>
        {
            // 加载配置文件
            var config = configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext();

            if (configAction != null) configAction.Invoke(config);
            else
            {
                // 判断是否有输出配置
                var hasWriteTo = context.Configuration["Serilog:WriteTo:0:Name"];
                if (hasWriteTo == null)
                {
                    config.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                      .WriteTo.File(Path.Combine(!isSingleFileEnvironment ? AppDomain.CurrentDomain.BaseDirectory : AppContext.BaseDirectory, "logs", "application..log"), LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, encoding: Encoding.UTF8);
                }
            }
        });

        return hostBuilder;
    }

    /// <summary>
    /// 添加默认日志拓展
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configAction"></param>
    /// <returns></returns>
    public static IHostBuilder UseSerilogDefault(this IHostBuilder builder, Action<LoggerConfiguration> configAction = default)
    {
        // 判断是否是单文件环境
        var isSingleFileEnvironment = string.IsNullOrWhiteSpace(Assembly.GetEntryAssembly().Location);

        builder.UseSerilog((context, configuration) =>
        {
            // 加载配置文件
            var config = configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext();

            if (configAction != null) configAction.Invoke(config);
            else
            {
                // 判断是否有输出配置
                var hasWriteTo = context.Configuration["Serilog:WriteTo:0:Name"];
                if (hasWriteTo == null)
                {
                    config.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                      .WriteTo.File(Path.Combine(!isSingleFileEnvironment ? AppDomain.CurrentDomain.BaseDirectory : AppContext.BaseDirectory, "logs", "application..log"), LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, encoding: Encoding.UTF8);
                }
            }
        });

        return builder;
    }
#if !NET5_0
    /// <summary>
    /// 添加默认日志拓展
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configAction"></param>
    /// <returns></returns>
    public static WebApplicationBuilder UseSerilogDefault(this WebApplicationBuilder builder, Action<LoggerConfiguration> configAction = default)
    {
        builder.Host.UseSerilogDefault(configAction);

        return builder;
    }
#endif
}