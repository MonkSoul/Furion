using System;

namespace Fur.DependencyInjection
{
    /// <summary>
    /// 不被扫描和发现的特性
    /// </summary>
    /// <remarks>用于程序集扫描类型或方法时候</remarks>
    [SkipScan, AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Enum)]
    public class SkipScanAttribute : Attribute
    {
    }
}