using Furion.DependencyInjection;
using System;

namespace Furion.DataValidation
{
    /// <summary>
    /// 验证类型特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Enum)]
    public sealed class ValidationTypeAttribute : Attribute
    {
    }
}