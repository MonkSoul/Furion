// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// Schedule UI 配置选项
/// </summary>
[SuppressSniffer]
public sealed class ScheduleUIOptions
{
    /// <summary>
    /// UI 入口地址
    /// </summary>
    /// <remarks>需以 / 开头，结尾不包含 / </remarks>
    public string RequestPath { get; set; } = "/schedule";

    /// <summary>
    /// 启用目录浏览
    /// </summary>
    public bool EnableDirectoryBrowsing { get; set; } = false;

    /// <summary>
    /// 生产环境关闭
    /// </summary>
    /// <remarks>默认 false</remarks>
    public bool DisableOnProduction { get; set; } = false;

    /// <summary>
    /// 二级虚拟目录
    /// </summary>
    /// <remarks>需以 / 开头，结尾不包含 / </remarks>
    public string VirtualPath { get; set; }
}