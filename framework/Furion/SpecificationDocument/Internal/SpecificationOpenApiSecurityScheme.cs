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

using Microsoft.OpenApi.Models;

namespace Furion.SpecificationDocument
{
    /// <summary>
    /// 规范化稳定安全配置
    /// </summary>
    public sealed class SpecificationOpenApiSecurityScheme : OpenApiSecurityScheme
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SpecificationOpenApiSecurityScheme()
        {
        }

        /// <summary>
        /// 唯一Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 安全需求
        /// </summary>
        public SpecificationOpenApiSecurityRequirementItem Requirement { get; set; }
    }
}