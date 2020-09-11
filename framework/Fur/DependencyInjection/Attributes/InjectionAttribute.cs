// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System;

namespace Fur.DependencyInjection
{
    /// <summary>
    /// 设置依赖注入方式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectionAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InjectionAttribute()
        {
            Injection = InjectionOptions.TryAdd;
            InjectionScope = InjectionScopeOptions.FirstOneInterface;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="injection">注册方式</param>
        public InjectionAttribute(InjectionOptions injection)
        {
            Injection = injection;
        }

        /// <summary>
        /// 注册方式
        /// </summary>
        public InjectionOptions Injection { get; set; }

        /// <summary>
        /// 注册访问
        /// </summary>
        public InjectionScopeOptions InjectionScope { get; set; }
    }
}