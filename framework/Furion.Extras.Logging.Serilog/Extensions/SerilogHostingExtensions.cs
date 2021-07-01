// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Text;

namespace Microsoft.Extensions.Hosting
{
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
        public static IWebHostBuilder UseSerilogDefault(this IWebHostBuilder hostBuilder, Action<LoggerConfiguration> configAction = default)
        {
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
                          .WriteTo.File(Path.Combine("logs", "application.log"), LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, encoding: Encoding.UTF8);
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
                          .WriteTo.File(Path.Combine("logs", "application.log"), LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, encoding: Encoding.UTF8);
                    }
                }
            });

            return builder;
        }
    }
}