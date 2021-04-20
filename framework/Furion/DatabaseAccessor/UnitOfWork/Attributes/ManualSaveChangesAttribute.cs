using Furion.DependencyInjection;
using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 手动提交 SaveChanges
    /// <para>默认情况下，框架会自动在方法结束之时调用 SaveChanges 方法，贴此特性可以忽略该行为</para>
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public sealed class ManualSaveChangesAttribute : Attribute
    {
    }
}