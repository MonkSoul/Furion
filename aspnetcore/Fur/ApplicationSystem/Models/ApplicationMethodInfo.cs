using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.Models.ApplicationSystem
{
    /// <summary>
    /// 应用方法信息类
    /// </summary>
    public sealed class ApplicationMethodInfo
    {
        /// <summary>
        /// 方法
        /// </summary>
        public MethodInfo Method { get; set; }
        /// <summary>
        /// 方法自定义特性
        /// </summary>
        public IEnumerable<Attribute> CustomAttributes { get; set; }
    }
}
