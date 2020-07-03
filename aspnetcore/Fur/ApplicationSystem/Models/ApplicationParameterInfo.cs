using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.ApplicationSystem.Models
{
    /// <summary>
    /// 应用方法参数信息类
    /// </summary>
    public sealed class ApplicationParameterInfo
    {
        /// <summary>
        /// 所属程序集
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// 所属类型
        /// </summary>
        public Type DeclareType { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 自定义特性
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
    }
}