// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 安全定义需求子项
    /// </summary>
    [SkipScan]
    public sealed class SpecificationOpenApiSecurityRequirementItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SpecificationOpenApiSecurityRequirementItem()
        {
            Accesses = System.Array.Empty<string>();
        }

        /// <summary>
        /// 安全Schema
        /// </summary>
        public OpenApiSecurityScheme Scheme { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string[] Accesses { get; set; }
    }
}