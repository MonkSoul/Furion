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

using System.ComponentModel;

namespace Furion.DependencyInjection
{
    /// <summary>
    /// 注册范围
    /// </summary>
    [SuppressSniffer]
    public enum InjectionPatterns
    {
        /// <summary>
        /// 只注册自己
        /// </summary>
        [Description("只注册自己")]
        Self,

        /// <summary>
        /// 第一个接口
        /// </summary>
        [Description("只注册第一个接口")]
        FirstInterface,

        /// <summary>
        /// 自己和第一个接口，默认值
        /// </summary>
        [Description("自己和第一个接口")]
        SelfWithFirstInterface,

        /// <summary>
        /// 所有接口
        /// </summary>
        [Description("所有接口")]
        ImplementedInterfaces,

        /// <summary>
        /// 注册自己包括所有接口
        /// </summary>
        [Description("自己包括所有接口")]
        All
    }
}