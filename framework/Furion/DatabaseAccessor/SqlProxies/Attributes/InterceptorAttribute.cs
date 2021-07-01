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

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 代理拦截器
    /// </summary>
    /// <remarks>如果贴在静态方法中且 InterceptorId/MethodName 为空，则为全局拦截</remarks>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
    public class InterceptorAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InterceptorAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interceptorIds"></param>
        public InterceptorAttribute(params string[] interceptorIds)
        {
            InterceptorIds = interceptorIds;
        }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string[] InterceptorIds { get; set; }
    }
}