using Furion.DependencyInjection;
using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 跳过实体监听
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class)]
    public sealed class NotChangedListenerAttribute : Attribute
    {
    }
}