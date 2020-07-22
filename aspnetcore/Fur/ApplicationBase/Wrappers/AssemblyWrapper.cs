using System.Collections.Generic;
using System.Reflection;

namespace Fur.ApplicationBase.Wrappers
{
    /// <summary>
    /// 程序集包装类
    /// <code>sealed</code>
    /// <para>主要用来装载解决方案项目中程序集常用属性及附加属性， 避免重复反射读取</para>
    /// </summary>
    public sealed class AssemblyWrapper
    {
        /// <summary>
        /// 被包装程序集
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// 程序集名称
        /// <example>
        /// <para>例如：</para>
        /// <code>AssemblyWrapper</code>
        /// </example>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 程序集完整名称
        /// <example>
        /// <para>例如：</para>
        /// <code>Fur.ApplicationBase.Wrappers.AssemblyWrapper</code>
        /// </example>
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 当前程序集下所有的公开类型包装类集合。参见：<see cref="Fur.ApplicationBase.Wrappers.TypeWrapper"/>
        /// <para>包含：</para>
        /// <list type="bullet">
        /// <item>
        /// <description>普通类、静态类、抽象类、密封类</description>
        /// </item>
        /// </list>
        /// <para>不包含：</para>
        /// <list type="bullet">
        /// <item>
        /// <description>接口类、贴有 [<see cref="Fur.ApplicationBase.Attributes.NonWrapperAttribute"/>] 特性的类型。参见：<see cref="Fur.ApplicationBase.Attributes.NonWrapperAttribute"/></description>
        /// </item>
        /// </list>
        /// </summary>
        public IEnumerable<TypeWrapper> PublicClassTypes { get; set; }
    }
}