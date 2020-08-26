using Fur.DynamicApiController;
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
        /// 方法错误异常特性
        /// </summary>
        internal static readonly ConcurrentDictionary<MethodInfo, MethodIfException> ErrorMethods;

        /// <summary>
        /// 错误消息字典
        /// </summary>
        internal static readonly Dictionary<object, string> ErrorCodeMessages;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static Oops()
        {
            ErrorMethods = new ConcurrentDictionary<MethodInfo, MethodIfException>();
            ErrorCodeMessages = GetErrorCodeMessages();
        }

        /// <summary>
        /// 抛出一个异常
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Exception Made(object errorCode, params object[] args)
        {
            return new Exception(GetErrorCodeMessage(errorCode, args));
        }

        /// <summary>
        /// 抛出一个异常
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Exception Made(object errorCode, Type exceptionType, params object[] args)
        {
            return Activator.CreateInstance(exceptionType, new object[] { GetErrorCodeMessage(errorCode, args) }) as Exception;
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
                ? (ErrorCodeMessages.GetValueOrDefault(errorCode) ?? "Internal Server Error.")
                : ifExceptionAttribute.ErrorMessage;

            // 不带消息的格式化
            if (ifExceptionAttribute?.Args.Length > 0)
            {
                errorCodeMessage = string.Format(errorCodeMessage, ifExceptionAttribute.Args);
            }

            // 字符串格式化
            if (args.Length > 0) return string.Format(errorCodeMessage, args);

            return errorCodeMessage;
        }

        private static Dictionary<object, string> GetErrorCodeMessages()
        {
            // 获取错误代码提供器
            var exceptionErrorCodeProvider = App.ServiceProvider.GetService<IExceptionErrorCodeProvider>();

            // 解析所有错误状态码字段，该字段必须是公开的、静态的且贴有 [ExceptionMetadata] 特性
            var errorCodesFields = exceptionErrorCodeProvider.Definitions
                .SelectMany(u => u.GetFields(BindingFlags.Public | BindingFlags.Static).Where(u => u.IsDefined(typeof(ExceptionMetadataAttribute))));

            // 构建错误码和消息字典集合
            var errorCodeMessages = errorCodesFields
                .Select(u => new
                {
                    Key = u.GetRawConstantValue(),
                    Value = u.GetCustomAttribute<ExceptionMetadataAttribute>().ErrorMessage
                })
                .ToDictionary(u => u.Key, u => u.Value);

            return errorCodeMessages;
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