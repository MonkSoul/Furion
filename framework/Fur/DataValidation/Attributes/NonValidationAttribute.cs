using Fur.DependencyInjection;
using System;

namespace Fur.DataValidation
{
    /// <summary>
    /// 跳过验证
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class NonValidationAttribute : Attribute
    {
    }
}