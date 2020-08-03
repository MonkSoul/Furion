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
        public IEnumerable<AssemblyInflation> Assemblies { get; internal set; }

        /// <summary>
        /// 类型集合
        /// </summary>
        /// <remarks>
        /// <para>不包含：非公开、接口、枚举、或贴有 <see cref="NonInflatedAttribute"/> 特性的类型</para>
        /// </remarks>
        public IEnumerable<TypeInflation> ClassTypes { get; internal set; }

        /// <summary>
        /// 方法集合
        /// </summary>
        /// <remarks>
        /// <para>包含：公开实例方法、公开静态方法</para>
        /// </remarks>
        public IEnumerable<MethodInflation> Methods { get; internal set; }
    }
}