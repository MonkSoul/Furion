using Fur.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// 禁止规范化处理
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public sealed class NonUnifyAttribute : Attribute
    {
    }
}