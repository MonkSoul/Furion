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

using Fur.ConfigurableOptions;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Linq;

namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 规范化文档配置选项
    /// </summary>
    [OptionsSettings("SpecificationDocumentSettings")]
    public sealed class SpecificationDocumentSettingsOptions : IConfigurableOptions<SpecificationDocumentSettingsOptions>
    {
        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocumentTitle { get; set; }

        /// <summary>
        /// 默认分组名
        /// </summary>
        public string DefaultGroupName { get; set; }

        /// <summary>
        /// 启用授权支持
        /// </summary>
        public bool? EnableAuthorized { get; set; }

        /// <summary>
        /// 格式化为V2版本
        /// </summary>
        public bool? FormatAsV2 { get; set; }

        /// <summary>
        /// 配置规范化文档地址
        /// </summary>
        public string RoutePrefix { get; set; }

        /// <summary>
        /// 文档展开设置
        /// </summary>
        public DocExpansion? DocExpansionState { get; set; }

        /// <summary>
        /// XML 描述文件
        /// </summary>
        public string[] XmlComments { get; set; }

        /// <summary>
        /// 分组信息
        /// </summary>
        public SpecificationOpenApiInfo[] GroupOpenApiInfos { get; set; }

        /// <summary>
        /// 安全定义
        /// </summary>
        public SpecificationOpenApiSecurityScheme[] SecurityDefinitions { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(SpecificationDocumentSettingsOptions options, IConfiguration configuration)
        {
            options.DocumentTitle ??= "Specification Api Document";
            options.DefaultGroupName ??= "Default";
            options.FormatAsV2 ??= false;
            options.RoutePrefix ??= "api";
            options.DocExpansionState ??= DocExpansion.List;
            XmlComments ??= App.Assemblies.Where(u => u.GetName().Name != "Fur").Select(t => t.GetName().Name).ToArray();
            GroupOpenApiInfos ??= new SpecificationOpenApiInfo[]
            {
                new SpecificationOpenApiInfo()
                {
                    Group=options.DefaultGroupName
                }
            };
            SecurityDefinitions ??= Array.Empty<SpecificationOpenApiSecurityScheme>();
        }
    }
}