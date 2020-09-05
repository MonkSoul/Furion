// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using Microsoft.OpenApi.Models;

namespace Fur.SpecificationDocument
{
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