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

using Furion.DependencyInjection;
using System;

namespace Furion.InstantMessaging
{
    /// <summary>
    /// 即时通信集线器配置特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
    public sealed class MapHubAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pattern"></param>
        public MapHubAttribute(string pattern)
        {
            Pattern = pattern;
        }

        /// <summary>
        /// 配置终点路由地址
        /// </summary>
        public string Pattern { get; set; }
    }
}