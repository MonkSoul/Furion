// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常元数据特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ErrorCodeItemMetadataAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="args">格式化参数</param>
        public ErrorCodeItemMetadataAttribute(string errorMessage, params object[] args)
        {
            ErrorMessage = Oops.FormatErrorMessage(errorMessage, args);
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public object ErrorCode { get; set; }
    }
}