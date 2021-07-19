// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.14.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Furion.SpecificationDocument
{
    /// <summary>
    /// 规范化结果中间件配置选项
    /// </summary>
    public sealed class SpecificationDocumentMiddlewareOptions
    {
        /// <summary>
        /// Swagger 配置
        /// </summary>
        public Action<SwaggerOptions> SwaggerConfigure { get; set; }

        /// <summary>
        /// Swagger UI 配置
        /// </summary>
        public Action<SwaggerUIOptions> SwaggerUIConfigure { get; set; }
    }
}