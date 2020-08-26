using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常元数据特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ExceptionMetadataAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="args">格式化参数</param>
        public ExceptionMetadataAttribute(string errorMessage, params object[] args)
            => ErrorMessage = args.Length > 0 ? string.Format(errorMessage, args) : errorMessage;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}