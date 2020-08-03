using Fur.AppCore.Attributes;
using System.Collections.Generic;

namespace Fur.AppCore.Wrappers
{
    /// <summary>
    /// 应用包装类
    /// <para>提供常用的程序集、公开类、公开方法属性</para>
    /// </summary>
    [NonWrapper]
    public sealed class ApplicationWrapper
    {
        /// <summary>
        /// 所有程序集包装类集合
        /// </summary>
        public IEnumerable<AssemblyWrapper> AssemblyWrappers { get; internal set; }

        /// <summary>
        /// 所有公开类型包装类集合
        /// </summary>
        public IEnumerable<TypeWrapper> PublicClassTypeWrappers { get; internal set; }

        /// <summary>
        /// 所有公开方法包装类集合
        /// </summary>
        public IEnumerable<MethodWrapper> PublicMethodWrappers { get; internal set; }
    }
}