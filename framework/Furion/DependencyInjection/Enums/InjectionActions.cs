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
    /// 服务注册方式
    /// </summary>
    [SuppressSniffer]
    public enum InjectionActions
    {
        /// <summary>
        /// 如果存在则覆盖
        /// </summary>
        [Description("存在则覆盖")]
        Add,

        /// <summary>
        /// 如果存在则跳过，默认方式
        /// </summary>
        [Description("存在则跳过")]
        TryAdd
    }
}