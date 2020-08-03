using Fur.AppCore.Attributes;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.AppCore.Inflations
{
    /// <summary>
    /// 程序集包装器
    /// </summary>
    [NonInflated]
    public sealed class AssemblyInflation
    {
        /// <summary>
        /// 被包装程序集
        /// </summary>
        public Assembly ThisAssembly { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 程序集完整名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 包含子类型集合
        /// </summary>
        /// <remarks>
        /// <para>不包含：非公开、接口、枚举、或贴有 <see cref="NonInflatedAttribute"/> 特性的类型</para>
        /// </remarks>
        public IEnumerable<TypeInflation> SubClassTypes { get; set; }
    }
}