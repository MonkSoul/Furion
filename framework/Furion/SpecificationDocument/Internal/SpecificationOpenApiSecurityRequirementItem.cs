// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.OpenApi.Models;

namespace Furion.SpecificationDocument;

/// <summary>
/// 安全定义需求子项
/// </summary>
[SuppressSniffer]
public sealed class SpecificationOpenApiSecurityRequirementItem
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SpecificationOpenApiSecurityRequirementItem()
    {
        Accesses = System.Array.Empty<string>();
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