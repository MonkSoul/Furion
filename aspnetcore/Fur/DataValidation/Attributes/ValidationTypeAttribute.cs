using System;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class ValidationTypeAttribute : Attribute
    {
    }
}