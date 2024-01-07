// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 接口参数位置
/// </summary>
[SuppressSniffer]
public enum ApiSeats
{
    /// <summary>
    /// 控制器之前
    /// </summary>
    [Description("控制器之前")]
    ControllerStart,

    /// <summary>
    /// 控制器之后
    /// </summary>
    [Description("控制器之后")]
    ControllerEnd,

    /// <summary>
    /// 行为之前
    /// </summary>
    [Description("行为之前")]
    ActionStart,

    /// <summary>
    /// 行为之后
    /// </summary>
    [Description("行为之后")]
    ActionEnd
}