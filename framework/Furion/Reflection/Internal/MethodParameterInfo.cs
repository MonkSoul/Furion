// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using System.Reflection;

namespace Furion.Reflection
{
    /// <summary>
    /// 方法参数信息
    /// </summary>
    internal class MethodParameterInfo
    {
        /// <summary>
        /// 参数
        /// </summary>
        internal ParameterInfo Parameter { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        internal object Value { get; set; }
    }
}