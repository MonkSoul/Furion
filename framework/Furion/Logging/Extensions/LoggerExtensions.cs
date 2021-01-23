using Furion;
using Furion.DependencyInjection;
using System;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 日志拓展类
    /// </summary>
    [SkipScan]
    public static class LoggerExtensions
    {
        /// <summary>
        /// 动态日志级别
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDynamic(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(GetConfigureLogLevel(), eventId, exception, message, args);
        }

        /// <summary>
        /// 动态日志级别
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDynamic(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(GetConfigureLogLevel(), eventId, message, args);
        }

        /// <summary>
        /// 动态日志级别
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDynamic(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(GetConfigureLogLevel(), exception, message, args);
        }

        /// <summary>
        /// 动态日志级别
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDynamic(this ILogger logger, string message, params object[] args)
        {
            logger.Log(GetConfigureLogLevel(), message, args);
        }

        /// <summary>
        /// 获取配置动态日志级别
        /// </summary>
        /// <returns></returns>
        private static LogLevel GetConfigureLogLevel()
        {
            var logLevel = App.Configuration["AppSettings:DynamicLogLevel"];
            return string.IsNullOrEmpty(logLevel) ? LogLevel.Information : Enum.Parse<LogLevel>(logLevel);
        }
    }
}