using Fur.Options;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 规范化文档配置选项
    /// </summary>
    [OptionsSettings("AppSettings:SpecificationDocumentSettings")]
    public sealed class SpecificationDocumentSettingsOptions : IAppOptions<SpecificationDocumentSettingsOptions>
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
        /// XML 描述文件
        /// </summary>
        public string[] XmlComments { get; set; }

        /// <summary>
        /// 分组信息
        /// </summary>
        public IEnumerable<OpenApiInfo> GroupInfos { get; set; }

        /// <summary>
        /// 启用授权支持
        /// </summary>
        public bool? EnableAuthorized { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        public void PostConfigure(SpecificationDocumentSettingsOptions options)
        {
            options.DocumentTitle ??= $"{nameof(Fur)} Specification Document";
            options.DefaultGroupName ??= "Default";
        }
    }
}