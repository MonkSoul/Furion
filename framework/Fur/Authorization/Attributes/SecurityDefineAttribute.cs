// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using System;

namespace Fur.Authorization
{
    /// <summary>
    /// 安全定义特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class SecurityDefineAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public SecurityDefineAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 名称，必须是系统唯一
        /// </summary>
        public string Name { get; set; }
    }
}