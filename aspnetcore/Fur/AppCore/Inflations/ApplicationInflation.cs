using Fur.AppCore.Attributes;
using System.Collections.Generic;

namespace Fur.AppCore.Inflations
{
    /// <summary>
    /// 应用包装器
    /// </summary>
    [NonInflated]
    internal sealed class ApplicationInflation
    {
        /// <summary>
        /// 应用程序集
        /// </summary>
        /// <remarks>
        /// 排除第三方下载的Nuget Package包但不排除 Fur官方的Nuget Package包
        /// </remarks>
        internal IEnumerable<AssemblyInflation> Assemblies { get; set; }

        /// <summary>
        /// 类型集合
        /// </summary>
        /// <remarks>
        /// <para>不包含：非公开、接口、枚举、或贴有 <see cref="NonInflatedAttribute"/> 特性的类型</para>
        /// </remarks>
        internal IEnumerable<TypeInflation> ClassTypes { get; set; }

        /// <summary>
        /// 方法集合
        /// </summary>
        /// <remarks>
        /// <para>包含：公开实例方法、公开静态方法</para>
        /// </remarks>
        internal IEnumerable<MethodInflation> Methods { get; set; }
    }
}