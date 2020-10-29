using System;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证消息类型提供器
    /// </summary>
    public interface IValidationMessageTypeProvider
    {
        /// <summary>
        /// 验证消息类型定义
        /// </summary>
        Type[] Definitions { get; }
    }
}