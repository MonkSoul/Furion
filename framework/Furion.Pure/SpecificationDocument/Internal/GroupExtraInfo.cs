// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.SpecificationDocument;

/// <summary>
/// 分组附加信息
/// </summary>
[SuppressSniffer]
public sealed class GroupExtraInfo
{
    /// <summary>
    /// 分组名
    /// </summary>
    public string Group { get; internal set; }

    /// <summary>
    /// 分组排序
    /// </summary>
    public int Order { get; internal set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool Visible { get; internal set; }
}