using System;

namespace Fur.Attributes
{
    /// <summary>
    /// 被贴此特性的类型、属性、方法都将在应用启动时被忽略装载包装
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property), NonInflated]
    public sealed class NonInflatedAttribute : Attribute { }
}