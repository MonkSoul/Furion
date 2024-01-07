// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Logging;

/// <summary>
/// 控制台颜色结构
/// </summary>
internal readonly struct ConsoleColors
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="foreground"></param>
    /// <param name="background"></param>
    public ConsoleColors(ConsoleColor? foreground, ConsoleColor? background)
    {
        Foreground = foreground;
        Background = background;
    }

    /// <summary>
    /// 前景色
    /// </summary>
    public ConsoleColor? Foreground { get; }

    /// <summary>
    /// 背景色
    /// </summary>
    public ConsoleColor? Background { get; }
}