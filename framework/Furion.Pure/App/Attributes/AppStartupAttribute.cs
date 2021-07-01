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

namespace Furion
{
    /// <summary>
    /// 注册服务启动配置
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
    public class AppStartupAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="order"></param>
        public AppStartupAttribute(int order)
        {
            Order = order;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
    }
}