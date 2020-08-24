using Fur.DynamicApiController;
using Microsoft.OpenApi.Models;

namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 规范化文档开放接口信息
    /// </summary>
    public sealed class SpecificationOpenApiInfo : OpenApiInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group"></param>
        public SpecificationOpenApiInfo(string group)
        {
            this.Group = group;
            base.Title ??= string.Join(' ', Penetrates.SplitCamelCase(group));
            base.Version = "1.0.0";
        }

        /// <summary>
        /// 所属组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Order { get; set; }
    }
}