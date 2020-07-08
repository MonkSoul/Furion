using System;

namespace Fur.ApplicationBase.Attributes
{
    /// <summary>
    /// 禁止应用启动时被包装
    /// <para>贴了 [<see cref="NonWrapperAttribute"/>] 特性后，该类型、方法、属性 将不被包装</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class NonWrapperAttribute : Attribute { }
}