using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Furion.Logging
{
    /// <summary>
    /// 字符串日志输出拓展
    /// </summary>
    [SkipScan]
    public static class StringLoggingExtensions
    {
        /// <summary>
        /// LogInformation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="args"></param>
        public static void LogInformation(this string message, string categoryName, params object[] args)
        {
            GetLogger(categoryName)?.LogInformation(message, args);
        }

        /// <summary>
        /// LogInformation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogInformation(this string message, string categoryName, EventId eventId, params object[] args)
        {
            GetLogger(categoryName)?.LogInformation(eventId, message, args);
        }

        /// <summary>
        /// LogInformation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogInformation(this string message, string categoryName, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogInformation(exception, message, args);
        }

        /// <summary>
        /// LogInformation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogInformation(this string message, string categoryName, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogInformation(eventId, exception, message, args);
        }

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="args"></param>
        public static void LogWarning(this string message, string categoryName, params object[] args)
        {
            GetLogger(categoryName)?.LogWarning(message, args);
        }

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogWarning(this string message, string categoryName, EventId eventId, params object[] args)
        {
            GetLogger(categoryName)?.LogWarning(eventId, message, args);
        }

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogWarning(this string message, string categoryName, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogWarning(exception, message, args);
        }

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogWarning(this string message, string categoryName, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogWarning(eventId, exception, message, args);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="args"></param>
        public static void LogError(this string message, string categoryName, params object[] args)
        {
            GetLogger(categoryName)?.LogError(message, args);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogError(this string message, string categoryName, EventId eventId, params object[] args)
        {
            GetLogger(categoryName)?.LogError(eventId, message, args);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogError(this string message, string categoryName, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogError(exception, message, args);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogError(this string message, string categoryName, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogError(eventId, exception, message, args);
        }

        /// <summary>
        /// LogTrace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="args"></param>
        public static void LogTrace(this string message, string categoryName, params object[] args)
        {
            GetLogger(categoryName)?.LogTrace(message, args);
        }

        /// <summary>
        /// LogTrace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogTrace(this string message, string categoryName, EventId eventId, params object[] args)
        {
            GetLogger(categoryName)?.LogTrace(eventId, message, args);
        }

        /// <summary>
        /// LogTrace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogTrace(this string message, string categoryName, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogTrace(exception, message, args);
        }

        /// <summary>
        /// LogTrace
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogTrace(this string message, string categoryName, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogTrace(eventId, exception, message, args);
        }

        /// <summary>
        /// LogCritical
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="args"></param>
        public static void LogCritical(this string message, string categoryName, params object[] args)
        {
            GetLogger(categoryName)?.LogCritical(message, args);
        }

        /// <summary>
        /// LogCritical
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogCritical(this string message, string categoryName, EventId eventId, params object[] args)
        {
            GetLogger(categoryName)?.LogCritical(eventId, message, args);
        }

        /// <summary>
        /// LogCritical
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogCritical(this string message, string categoryName, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogCritical(exception, message, args);
        }

        /// <summary>
        /// LogCritical
        /// </summary>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogCritical(this string message, string categoryName, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.LogCritical(eventId, exception, message, args);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="logLevel"></param>
        /// <param name="args"></param>
        public static void Log<TClass>(this string message, string categoryName, LogLevel logLevel, params object[] args)
        {
            GetLogger(categoryName)?.Log(logLevel, message, args);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void Log<TClass>(this string message, string categoryName, LogLevel logLevel, EventId eventId, params object[] args)
        {
            GetLogger(categoryName)?.Log(logLevel, eventId, message, args);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void Log<TClass>(this string message, string categoryName, LogLevel logLevel, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.Log(logLevel, exception, message, args);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void Log<TClass>(this string message, string categoryName, LogLevel logLevel, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger(categoryName)?.Log(logLevel, eventId, exception, message, args);
        }

        /// <summary>
        /// LogInformation
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogInformation<TClass>(this string message, params object[] args)
        {
            GetLogger<TClass>()?.LogInformation(message, args);
        }

        /// <summary>
        /// LogInformation
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogInformation<TClass>(this string message, EventId eventId, params object[] args)
        {
            GetLogger<TClass>()?.LogInformation(eventId, message, args);
        }

        /// <summary>
        /// LogInformation
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogInformation<TClass>(this string message, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogInformation(exception, message, args);
        }

        /// <summary>
        /// LogInformation
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogInformation<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogInformation(eventId, exception, message, args);
        }

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogWarning<TClass>(this string message, params object[] args)
        {
            GetLogger<TClass>()?.LogWarning(message, args);
        }

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogWarning<TClass>(this string message, EventId eventId, params object[] args)
        {
            GetLogger<TClass>()?.LogWarning(eventId, message, args);
        }

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogWarning<TClass>(this string message, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogWarning(exception, message, args);
        }

        /// <summary>
        /// LogWarning
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogWarning<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogWarning(eventId, exception, message, args);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogError<TClass>(this string message, params object[] args)
        {
            GetLogger<TClass>()?.LogError(message, args);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogError<TClass>(this string message, EventId eventId, params object[] args)
        {
            GetLogger<TClass>()?.LogError(eventId, message, args);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogError<TClass>(this string message, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogError(exception, message, args);
        }

        /// <summary>
        /// LogError
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogError<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogError(eventId, exception, message, args);
        }

        /// <summary>
        /// LogTrace
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogTrace<TClass>(this string message, params object[] args)
        {
            GetLogger<TClass>()?.LogTrace(message, args);
        }

        /// <summary>
        /// LogTrace
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogTrace<TClass>(this string message, EventId eventId, params object[] args)
        {
            GetLogger<TClass>()?.LogTrace(eventId, message, args);
        }

        /// <summary>
        /// LogTrace
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogTrace<TClass>(this string message, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogTrace(exception, message, args);
        }

        /// <summary>
        /// LogTrace
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogTrace<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogTrace(eventId, exception, message, args);
        }

        /// <summary>
        /// LogCritical
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogCritical<TClass>(this string message, params object[] args)
        {
            GetLogger<TClass>()?.LogCritical(message, args);
        }

        /// <summary>
        /// LogCritical
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void LogCritical<TClass>(this string message, EventId eventId, params object[] args)
        {
            GetLogger<TClass>()?.LogCritical(eventId, message, args);
        }

        /// <summary>
        /// LogCritical
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogCritical<TClass>(this string message, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogCritical(exception, message, args);
        }

        /// <summary>
        /// LogCritical
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void LogCritical<TClass>(this string message, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.LogCritical(eventId, exception, message, args);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="args"></param>
        public static void Log<TClass>(this string message, LogLevel logLevel, params object[] args)
        {
            GetLogger<TClass>()?.Log(logLevel, message, args);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="args"></param>
        public static void Log<TClass>(this string message, LogLevel logLevel, EventId eventId, params object[] args)
        {
            GetLogger<TClass>()?.Log(logLevel, eventId, message, args);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void Log<TClass>(this string message, LogLevel logLevel, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.Log(logLevel, exception, message, args);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public static void Log<TClass>(this string message, LogLevel logLevel, EventId eventId, Exception exception, params object[] args)
        {
            GetLogger<TClass>()?.Log(logLevel, eventId, exception, message, args);
        }

        /// <summary>
        /// 获取日志操作对象
        /// </summary>
        /// <typeparam name="TClass">类型</typeparam>
        /// <returns></returns>
        private static ILogger<TClass> GetLogger<TClass>()
        {
            return HttpContextLocal.Current()?.RequestServices?.GetService<ILogger<TClass>>();
        }

        /// <summary>
        /// 获取日志操作对象
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        private static ILogger GetLogger(string categoryName)
        {
            return HttpContextLocal.Current()?.RequestServices?.GetService<ILoggerFactory>()?.CreateLogger(categoryName);
        }
    }
}