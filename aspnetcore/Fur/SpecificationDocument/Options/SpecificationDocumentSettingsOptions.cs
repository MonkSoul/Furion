using Fur.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
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
        public IEnumerable<SpecificationOpenApiInfo> GroupOpenApiInfos { get; set; }

        /// <summary>
        /// 是否有分组配置
        /// </summary>
        internal bool HasGroups { get; private set; }

        /// <summary>
        /// 默认描述信息
        /// </summary>
        private const string defaultDescription = "OpenApi Document";

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        public void PostConfigure(SpecificationDocumentSettingsOptions options)
        {
            options.DocumentTitle ??= $"{nameof(Fur)} {defaultDescription}";
            options.DefaultGroupName ??= "Default";
            options.FormatAsV2 ??= false;
            options.RoutePrefix ??= string.Empty;
            options.DocExpansionState ??= DocExpansion.List;
            XmlComments ??= new string[]
           {
                "Fur.Application",
                "Fur.Web.Entry",
                "Fur.Web.Core"
           };
            GroupOpenApiInfos ??= new List<SpecificationOpenApiInfo>
            {
                new SpecificationOpenApiInfo(options.DefaultGroupName)
                {
                    Title=$"{options.DefaultGroupName} {defaultDescription}"
                }
            };
        }
    }
}