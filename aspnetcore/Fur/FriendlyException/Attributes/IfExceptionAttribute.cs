using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常复写特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class IfExceptionAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="args">格式化参数</param>
        public IfExceptionAttribute(object errorCode, params object[] args)
        {
            ErrorCode = errorCode;
            Args = args;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="args">格式化参数</param>
        public IfExceptionAttribute(object errorCode, string errorMessage, params object[] args)
        {
            ErrorCode = errorCode;
            ErrorMessage = args.Length > 0 ? string.Format(errorMessage, args) : errorMessage;
            Args = args;
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public object ErrorCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 格式化参数
        /// </summary>
        public object[] Args { get; set; }
    }
}