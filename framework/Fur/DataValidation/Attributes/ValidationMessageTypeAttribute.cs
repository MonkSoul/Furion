using Fur.DependencyInjection;
using System;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证消息类型特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Enum)]
    public sealed class ValidationMessageTypeAttribute : Attribute
    {
    }
}