using System;

namespace Furion.DependencyInjection
{
    /// <summary>
    /// 跳过全局代理
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class)]
    public class SkipProxyAttribute : Attribute
    {
    }
}