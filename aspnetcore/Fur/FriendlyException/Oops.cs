using Fur.FriendlyException.Attributes;
using Fur.MirrorController.Dependencies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 抛异常设置类
    /// </summary>

    public static class Oops
    {
        internal static readonly ConcurrentDictionary<(MethodInfo, int), OopsModel> ExceptionOopsModels;

        static Oops()
        {
            ExceptionOopsModels = new ConcurrentDictionary<(MethodInfo, int), OopsModel>();
        }

        /// <summary>
        /// 有bug
        /// </summary>
        /// <param name="exceptionCode">异常编码</param>
        /// <param name="exceptionType">异常类型</param>
        /// <returns></returns>
        public static Exception To(int exceptionCode, object[] args = null, Type exceptionType = null, int statusCode = 500)
        {
            var stackTrace = new StackTrace();
            var topStackFrame = stackTrace.GetFrames().FirstOrDefault(u => typeof(IMirrorControllerModel).IsAssignableFrom(u.GetMethod().DeclaringType) || typeof(ControllerBase).IsAssignableFrom(u.GetMethod().DeclaringType));
            var method = topStackFrame.GetMethod() as MethodInfo;
            var ifExceptionAttribute = method.GetCustomAttributes<IfExceptionAttribute>().FirstOrDefault(u => u.ExceptionCode == exceptionCode);

            ExceptionOopsModels.TryAdd((method, exceptionCode), new OopsModel
            {
                ExceptionCode = exceptionCode,
                ExceptionType = exceptionType,
                Args = args,
                IfException = ifExceptionAttribute,
                StatusCode = statusCode
            });

            return new Exception($"[[{exceptionCode}]]");
        }
    }
}