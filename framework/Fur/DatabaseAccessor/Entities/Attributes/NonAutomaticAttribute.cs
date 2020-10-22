using Fur.DependencyInjection;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 手动配置实体特性
    /// </summary>
    /// <remarks>支持类和方法</remarks>
    [SkipScan, AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NonAutomaticAttribute : Attribute
    {
    }
}