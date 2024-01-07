// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.OpenApi.Models;

namespace Furion.SpecificationDocument;

/// <summary>
/// 规范化文档开放接口信息
/// </summary>
[SuppressSniffer]
public sealed class SpecificationOpenApiInfo : OpenApiInfo
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SpecificationOpenApiInfo()
    {
        Version = "1.0.0";
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
            //Title ??= string.Join(' ', _group.SplitCamelCase());
            Title ??= _group;
        }
    }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Order { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool? Visible { get; set; }

    /// <summary>
    /// 路由模板
    /// </summary>
    public string RouteTemplate { get; internal set; }
}