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
        public SpecificationOpenApiInfo()
        {
        }

        /// <summary>
        /// 分组私有字段
        /// </summary>
        private string _group;

        /// <summary>
        /// 所属组
        /// </summary>
        public string Group
        {
            get => _group;
            set
            {
                _group = value;
                Title ??= string.Join(' ', Penetrates.SplitCamelCase(_group));
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Order { get; set; }
    }
}