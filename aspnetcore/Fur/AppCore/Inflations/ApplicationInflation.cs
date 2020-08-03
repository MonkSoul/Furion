using Fur.AppCore.Attributes;
using System.Collections.Generic;

namespace Fur.AppCore.Inflations
{
    /// <summary>
    /// 应用包装类
    /// <para>提供常用的程序集、公开类、公开方法属性</para>
    /// </summary>
    [NonInflated]
    public sealed class ApplicationInflation
    {
        /// <summary>
        /// 所有程序集包装类集合
        /// </summary>
        public IEnumerable<AssemblyInflation> AssemblyWrappers { get; internal set; }

        /// <summary>
        /// 所有公开类型包装类集合
        /// </summary>
        public IEnumerable<TypeInflation> PublicClassTypeWrappers { get; internal set; }

        /// <summary>
        /// 所有公开方法包装类集合
        /// </summary>
        public IEnumerable<MethodInflation> PublicMethodWrappers { get; internal set; }
    }
}