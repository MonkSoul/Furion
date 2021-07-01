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

namespace Microsoft.AspNetCore.Authorization
{
    /// <summary>
    /// 安全定义特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
    public class SecurityDefineAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SecurityDefineAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resourceId"></param>
        public SecurityDefineAttribute(string resourceId)
        {
            ResourceId = resourceId;
        }

        /// <summary>
        /// 资源Id，必须是唯一的
        /// </summary>
        public string ResourceId { get; set; }
    }
}