using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常元数据特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ErrorCodeMetadataAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="args">格式化参数</param>
        public ErrorCodeMetadataAttribute(string errorMessage, params object[] args)
            => ErrorMessage = Oops.FormatErrorMessage(errorMessage, args);

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}