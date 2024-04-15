// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

namespace Furion.Schedule;

/// <summary>
/// 作业触发器状态
/// </summary>
[SuppressSniffer]
public enum TriggerStatus : uint
{
    /// <summary>
    /// 积压
    /// </summary>
    /// <remarks>起始时间大于当前时间</remarks>
    Backlog = 0,

    /// <summary>
    /// 就绪
    /// </summary>
    Ready = 1,

    /// <summary>
    /// 正在运行
    /// </summary>
    Running = 2,

    /// <summary>
    /// 暂停
    /// </summary>
    Pause = 3,

    /// <summary>
    /// 阻塞
    /// </summary>
    /// <remarks>本该执行但是没有执行</remarks>
    Blocked = 4,

    /// <summary>
    /// 由失败进入就绪
    /// </summary>
    /// <remarks>运行错误当并未超出最大错误数，进入下一轮就绪</remarks>
    ErrorToReady = 5,

    /// <summary>
    /// 归档
    /// </summary>
    /// <remarks>结束时间小于当前时间</remarks>
    Archived = 6,

    /// <summary>
    /// 崩溃
    /// </summary>
    /// <remarks>错误次数超出了最大错误数</remarks>
    Panic = 7,

    /// <summary>
    /// 超限
    /// </summary>
    /// <remarks>运行次数超出了最大限制</remarks>
    Overrun = 8,

    /// <summary>
    /// 无触发时间
    /// </summary>
    /// <remarks>下一次执行时间为 null </remarks>
    Unoccupied = 9,

    /// <summary>
    /// 未启动
    /// </summary>
    NotStart = 10,

    /// <summary>
    /// 未知作业触发器
    /// </summary>
    /// <remarks>作业触发器运行时类型为 null</remarks>
    Unknown = 11,

    /// <summary>
    /// 未知作业处理程序
    /// </summary>
    /// <remarks>作业处理程序类型运行时类型为 null</remarks>
    Unhandled = 12
}