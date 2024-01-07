// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.OpenApi.Models;

namespace Furion.SpecificationDocument;

/// <summary>
/// 规范化文档安全配置
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