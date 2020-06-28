using System;
using System.Collections.Generic;

namespace Fur.Models.ApplicationSystem
{
    /// <summary>
    /// 应用类型信息类
    /// </summary>
    public sealed class ApplicationTypeInfo
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 类型公开实例方法
        /// </summary>
        public IEnumerable<ApplicationMethodInfo> PublicInstanceMethods { get; set; }
        /// <summary>
        /// 类型自定义特性
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
        /// <summary>
        /// 是否是泛型类型
        /// </summary>
        public bool IsGenericType { get; set; }
        /// <summary>
        /// 泛型类型参数
        /// </summary>
        public IEnumerable<Type> GenericArguments { get; set; }
    }
}
