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

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 远程请求参数拦截器
    /// </summary>
    /// <remarks>如果贴在静态方法中，则为全局拦截</remarks>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public class InterceptorAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        public InterceptorAttribute(InterceptorTypes type)
        {
            Type = type;
        }

        /// <summary>
        /// 拦截类型
        /// </summary>
        public InterceptorTypes Type { get; set; }
    }
}