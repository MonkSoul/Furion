using Fur.DynamicApiController;
using Fur.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 抛异常静态类
    /// </summary>
    public static class Oops
    {
        /// <summary>
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "errors";

        /// <summary>
        /// 方法错误异常特性
        /// </summary>
        internal static readonly ConcurrentDictionary<MethodInfo, MethodIfException> ErrorMethods;

        /// <summary>
        /// 错误消息字典
        /// </summary>
        internal static readonly Dictionary<string, string> ErrorCodeMessages;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Oops()
        {
            ErrorMethods = new ConcurrentDictionary<MethodInfo, MethodIfException>();
            ErrorCodeMessages = GetErrorCodeMessages();
        }

        /// <summary>
        /// 抛出字符串异常
        /// </summary>
        /// <param name="errorMessage">异常消息</param>
        /// <param name="args">String.Format 参数</param>
        /// <returns>异常实例</returns>
        public static Exception Oh(string errorMessage, params object[] args)
        {
            errorMessage = $"[Unknown] {errorMessage}";
            return new Exception(FormatErrorMessage(errorMessage, args));
        }

        /// <summary>
        /// 抛出字符串异常
        /// </summary>
        /// <param name="errorMessage">异常消息</param>
        /// <param name="exceptionType">具体异常类型</param>
        /// <param name="args">String.Format 参数</param>
        /// <returns>异常实例</returns>
        public static Exception Oh(string errorMessage, Type exceptionType, params object[] args)
        {
            errorMessage = $"[Unknown] {errorMessage}";
            return Activator.CreateInstance(exceptionType, new object[] { FormatErrorMessage(errorMessage, args) }) as Exception;
        }

        /// <summary>
        /// 抛出错误码异常
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <param name="args">String.Format 参数</param>
        /// <returns>异常实例</returns>
        public static Exception Oh(object errorCode, params object[] args)
        {
            return new Exception(GetErrorCodeMessage(errorCode, args));
        }

        /// <summary>
        /// 抛出错误码异常
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <param name="exceptionType">具体异常类型</param>
        /// <param name="args">String.Format 参数</param>
        /// <returns>异常实例</returns>
        public static Exception Oh(object errorCode, Type exceptionType, params object[] args)
        {
            return Activator.CreateInstance(exceptionType, new object[] { GetErrorCodeMessage(errorCode, args) }) as Exception;
        }

        /// <summary>
        /// 打印错误到 MiniProfiler 中
        /// </summary>
        /// <param name="exception"></param>
        internal static void PrintToMiniProfiler(Exception exception)
        {
            // 判断是否注入 MiniProfiler 组件
            if (App.Settings.InjectMiniProfiler != true) return;

            // 获取异常堆栈
            var traceFrame = new StackTrace(exception, true).GetFrame(0);

            // 获取出错的文件名
            var exceptionFileName = traceFrame.GetFileName();

            // 获取出错的行号
            var exceptionFileLineNumber = traceFrame.GetFileLineNumber();

            // 打印错误文件名和行号
            if (!string.IsNullOrEmpty(exceptionFileName) && exceptionFileLineNumber > 0)
            {
                App.PrintToMiniProfiler(MiniProfilerCategory, "Locator", $"{exceptionFileName}:line {exceptionFileLineNumber}", true);
            }

            // 打印完整的堆栈信息
            App.PrintToMiniProfiler(MiniProfilerCategory, "StackTrace", exception.ToString(), true);
        }

        /// <summary>
        /// 格式化错误消息
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="args">格式化参数</param>
        /// <returns></returns>
        internal static string FormatErrorMessage(string errorMessage, params object[] args)
        {
            return args == null || args.Length == 0 ? errorMessage : string.Format(errorMessage, args);
        }

        /// <summary>
        /// 获取错误码消息
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string GetErrorCodeMessage(object errorCode, params object[] args)
        {
            // 获取出错的方法
            var errorMethod = GetEndPointExceptionMethod();

            // 获取异常特性
            var ifExceptionAttribute = errorMethod.IfExceptionAttributes.FirstOrDefault(u => u.ErrorCode.Equals(errorCode));

            // 获取错误码消息
            var errorCodeMessage = ifExceptionAttribute == null || string.IsNullOrEmpty(ifExceptionAttribute.ErrorMessage)
                ? (ErrorCodeMessages.GetValueOrDefault(errorCode.ToString()) ?? "Internal Server Error.")
                : ifExceptionAttribute.ErrorMessage;

            // 采用 [IfException] 格式化参数覆盖
            errorCodeMessage = FormatErrorMessage(errorCodeMessage, ifExceptionAttribute?.Args);

            // 拼接状态码
            errorCodeMessage = $"[{errorCode}] {errorCodeMessage}";

            // 字符串格式化
            return FormatErrorMessage(errorCodeMessage, args);
        }

        /// <summary>
        /// 获取错误代码类型
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Type> GetErrorCodeTypes()
        {
            // 查找所有公开且是静态类且贴有 [ExceptionErrorCodes] 特性的类型
            var errorCodeTypes = App.Assemblies.SelectMany(u => u.GetTypes()
                    .Where(c => c.IsPublic && c.IsClass && c.IsSealed && c.IsAbstract && c.IsDefined(typeof(ErrorCodeTypeAttribute), true)))
                .ToList();

            // 获取错误代码提供器中定义的类型
            var errorCodeTypeProvider = App.ServiceProvider.GetService<IErrorCodeTypeProvider>();
            if (errorCodeTypeProvider is { Definitions: not null }) errorCodeTypes.AddRange(errorCodeTypeProvider.Definitions);

            return errorCodeTypes.Distinct();
        }

        /// <summary>
        /// 获取所有错误消息
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetErrorCodeMessages()
        {
            // 解析所有错误状态码字段，该字段必须是公开的、静态的且贴有 [ErrorCodeMetadata] 特性
            var errorCodeMessages = GetErrorCodeTypes()
                .SelectMany(u => u.GetFields(BindingFlags.Public | BindingFlags.Static).Where(u => u.IsDefined(typeof(ErrorCodeMetadataAttribute))))
                .Select(u => new
                {
                    Key = u.GetRawConstantValue(),
                    Value = u.GetCustomAttribute<ErrorCodeMetadataAttribute>().ErrorMessage
                })
               .ToDictionary(u => u.Key.ToString(), u => u.Value); ;

            // 加载配置文件状态码
            var errorCodeMessageSettings = App.GetOptions<ErrorCodeMessageSettingsOptions>();
            if (errorCodeMessageSettings is { Definitions: not null })
            {
                // 获取所有参数大于1的配置
                var fitErrorCodes = errorCodeMessageSettings.Definitions
                    .Where(u => u.Length > 1)
                    .ToDictionary(u => u[0].ToString(), u => FixErrorCodeSettingMessage(u));

                // 合并两个字典
                errorCodeMessages = (errorCodeMessages ?? new Dictionary<string, string>()).AddOrUpdate(fitErrorCodes);
            }

            return errorCodeMessages;
        }

        /// <summary>
        /// 处理异常配置数据
        /// </summary>
        /// <param name="errorCodes">错误消息配置对象</param>
        /// <remarks>
        /// 方式：数组第一个元素为错误码，第二个参数为错误消息，剩下的参数为错误码格式化字符串
        /// </remarks>
        /// <returns></returns>
        private static string FixErrorCodeSettingMessage(object[] errorCodes)
        {
            var args = errorCodes.Skip(2).ToArray();
            var errorMessage = errorCodes[1].ToString();
            return FormatErrorMessage(errorMessage, args);
        }

        /// <summary>
        /// 获取堆栈中顶部抛异常方法
        /// </summary>
        /// <returns></returns>
        private static MethodIfException GetEndPointExceptionMethod()
        {
            // 通过查找调用堆栈中错误的方法，该方法所在类型集成自 ControllerBase 类型或 IDynamicApiController接口
            var stackTrace = new StackTrace();
            var exceptionMethodFrame = stackTrace.GetFrames()
                .FirstOrDefault(u => typeof(ControllerBase).IsAssignableFrom(u.GetMethod().DeclaringType) || typeof(IDynamicApiController).IsAssignableFrom(u.GetMethod().DeclaringType));

            // 获取出错堆栈的方法对象
            var errorMethod = exceptionMethodFrame.GetMethod() as MethodInfo;

            // 判断是否已经缓存过该方法，避免重复解析
            var isCached = ErrorMethods.TryGetValue(errorMethod, out MethodIfException methodIfException);
            if (isCached) return methodIfException;

            // 组装方法异常对象
            methodIfException = new MethodIfException
            {
                ErrorMethod = errorMethod,
                IfExceptionAttributes = errorMethod.GetCustomAttributes<IfExceptionAttribute>()
            };

            // 存入缓存
            ErrorMethods.TryAdd(errorMethod, methodIfException);

            return methodIfException;
        }
    }
}