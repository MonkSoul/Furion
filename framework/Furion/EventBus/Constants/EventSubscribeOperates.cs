// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.EventBus;

/// <summary>
/// 事件订阅器操作选项
/// </summary>
/// <remarks>控制动态新增/删除事件订阅器</remarks>
internal enum EventSubscribeOperates
{
    /// <summary>
    /// 添加一条订阅器
    /// </summary>
    Append,

    /// <summary>
    /// 删除一条订阅器
    /// </summary>
    Remove
}