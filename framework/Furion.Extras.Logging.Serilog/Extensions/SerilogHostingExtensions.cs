// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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

#if !NET8_0 && !NET9_0
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
#endif

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