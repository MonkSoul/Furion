// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.OpenApi.Models;

namespace Fur.SpecificationDocument
{
    /// <summary>
    /// 安全定义需求子项
    /// </summary>
    public sealed class SpecificationOpenApiSecurityRequirementItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SpecificationOpenApiSecurityRequirementItem()
        {
            Accesses ??= new string[] { };
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