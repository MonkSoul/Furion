using System;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证消息类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ValidationMessageTypeAttribute : Attribute
    {
    }
}