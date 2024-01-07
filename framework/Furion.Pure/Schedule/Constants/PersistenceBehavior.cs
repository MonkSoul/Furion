// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 作业持久化行为
/// </summary>
[SuppressSniffer]
public enum PersistenceBehavior : uint
{
    /// <summary>
    /// 添加
    /// </summary>
    Appended = 0,

    /// <summary>
    /// 更新
    /// </summary>
    Updated = 1,

    /// <summary>
    /// 删除
    /// </summary>
    Removed = 2,
}