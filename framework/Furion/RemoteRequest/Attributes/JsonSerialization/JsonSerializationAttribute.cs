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
    /// JSON 序列化提供器
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class JsonSerializationAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="providerType"></param>
        public JsonSerializationAttribute(Type providerType)
        {
            ProviderType = providerType;
        }

        /// <summary>
        /// 提供器类型
        /// </summary>
        public Type ProviderType { get; set; }
    }
}