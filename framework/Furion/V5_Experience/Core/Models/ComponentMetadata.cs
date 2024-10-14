// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion;

/// <summary>
///     组件元数据
/// </summary>
internal readonly struct ComponentMetadata
{
    /// <summary>
    ///     <inheritdoc cref="ComponentMetadata" />
    /// </summary>
    /// <param name="name">组件名称</param>
    /// <param name="version">版本号</param>
    /// <param name="description">描述</param>
    internal ComponentMetadata(string name, Version? version, string? description)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        Version = version?.ToString();
        Description = description;

        NuGetPage = string.Format(Constants.NUGET_PACKAGE_PAGE, name, version?.ToString() ?? string.Empty);
        DocumentationPage = string.Format(Constants.FURION_COMPONENT_DOCS_PAGE, Name);
    }

    /// <summary>
    ///     组件名称
    /// </summary>
    internal string Name { get; init; }

    /// <summary>
    ///     版本号
    /// </summary>
    internal string? Version { get; init; }

    /// <summary>
    ///     描述
    /// </summary>
    internal string? Description { get; init; }

    /// <summary>
    ///     NuGet 地址
    /// </summary>
    internal string NuGetPage { get; init; }

    /// <summary>
    ///     文档地址
    /// </summary>
    internal string DocumentationPage { get; init; }
}