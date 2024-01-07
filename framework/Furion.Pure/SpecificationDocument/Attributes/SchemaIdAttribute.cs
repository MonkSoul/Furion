// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.SpecificationDocument;

/// <summary>
/// 解决规范化文档 SchemaId 冲突问题
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class SchemaIdAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="schemaId">自定义 SchemaId，只能是字母开头，只运行下划线_连接</param>
    public SchemaIdAttribute(string schemaId)
    {
        SchemaId = schemaId;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="schemaId">自定义 SchemaId</param>
    /// <param name="replace">默认在头部叠加，设置 true 之后，将直接使用 <see cref="SchemaId"/></param>
    public SchemaIdAttribute(string schemaId, bool replace)
    {
        SchemaId = schemaId;
        Replace = replace;
    }

    /// <summary>
    /// 自定义 SchemaId
    /// </summary>
    public string SchemaId { get; set; }

    /// <summary>
    /// 完全覆盖
    /// </summary>
    /// <remarks>默认在头部叠加，设置 true 之后，将直接使用 <see cref="SchemaId"/></remarks>
    public bool Replace { get; set; } = false;
}