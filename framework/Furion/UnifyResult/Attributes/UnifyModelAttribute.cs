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

namespace Furion.UnifyResult
{
    /// <summary>
    /// 规范化模型特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
    public class UnifyModelAttribute : Attribute
    {
        /// <summary>
        /// 规范化模型
        /// </summary>
        /// <param name="modelType"></param>
        public UnifyModelAttribute(Type modelType)
        {
            ModelType = modelType;
        }

        /// <summary>
        /// 模型类型（泛型）
        /// </summary>
        public Type ModelType { get; set; }
    }
}