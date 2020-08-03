using System;

namespace Fur.AppCore.Attributes
{
    /// <summary>
    /// 禁用应用启动时被扫描包装
    /// </summary>
    /// <remarks>
    /// <para>应用会在启动时扫描所有程序集、类型、方法、属性、参数进行包装，以在运行时快速查找</para>
    /// <para>但排除贴了 <see cref="NonInflatedAttribute"/> 特性的所有类型</para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property), NonInflated]
    public sealed class NonInflatedAttribute : Attribute { }
}