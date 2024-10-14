// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     指定当目标文件已存在时的行为
/// </summary>
public enum FileExistsBehavior
{
    /// <summary>
    ///     创建新文件
    /// </summary>
    /// <remarks>如果文件已存在则抛出异常。</remarks>
    CreateNew = 0,

    /// <summary>
    ///     覆盖现有文件
    /// </summary>
    Overwrite,

    /// <summary>
    ///     保留现有文件
    /// </summary>
    /// <remarks>不进行任何操作。</remarks>
    Skip
}