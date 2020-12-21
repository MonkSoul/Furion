using Serilog;
using System;
using System.IO;

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
        /// <param name="builder"></param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static IHostBuilder UseSerilogDefault(this IHostBuilder builder, Action<LoggerConfiguration> configAction = default)
        {
            builder.UseSerilog((hostingContext, services, loggerConfiguration) =>
            {
                // 加载配置文件
                var config = loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext();

                if (configAction != null) configAction.Invoke(config);
                else
                {
                    config.WriteTo.Console(
                            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                          .WriteTo.File(Path.Combine("logs", "log.txt"), rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true);
                }
            });

            return builder;
        }
    }
}