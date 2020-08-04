using Fur.AppCore.Attributes;
using System;

namespace Fur.FriendlyException.Attributes
{
    /// <summary>
    /// 支持复写默认异常提供器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true), NonInflated]
    public sealed class IfExceptionAttribute : Attribute
    {
        public IfExceptionAttribute(int exceptionCode, string message)
        {
            ExceptionCode = exceptionCode;
            Message = message;
        }

        /// <summary>
        /// 异常代码
        /// </summary>
        public int ExceptionCode { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }
    }
}
