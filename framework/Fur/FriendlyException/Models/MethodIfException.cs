// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using System.Collections.Generic;
using System.Reflection;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 方法异常类
    /// </summary>
    internal sealed class MethodIfException
    {
        /// <summary>
        /// 出异常的方法
        /// </summary>
        public MethodInfo ErrorMethod { get; set; }

        /// <summary>
        /// 异常特性
        /// </summary>
        public IEnumerable<IfExceptionAttribute> IfExceptionAttributes { get; set; }
    }
}