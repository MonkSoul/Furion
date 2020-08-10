using Fur.FriendlyException.Attributes;
using System;

namespace Fur.FriendlyException
{
    internal sealed class OopsModel
    {
        /// <summary>
        /// 异常状态码
        /// </summary>
        public int ExceptionCode { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public Type ExceptionType { get; set; }

        /// <summary>
        /// HTTP 状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 字符串格式化参数
        /// </summary>
        public object[] Args { get; set; }

        /// <summary>
        /// 异常特性
        /// </summary>
        public IfExceptionAttribute IfException { get; set; }
    }
}
