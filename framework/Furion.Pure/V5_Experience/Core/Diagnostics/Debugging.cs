// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Diagnostics;

namespace System;

/// <summary>
///     向事件管理器中输出事件信息
/// </summary>
internal static class Debugging
{
    /// <summary>
    ///     输出一行事件信息
    /// </summary>
    /// <param name="level">
    ///     <para>信息级别</para>
    ///     <list type="number">
    ///         <item>
    ///             <description>跟踪</description>
    ///         </item>
    ///         <item>
    ///             <description>信息</description>
    ///         </item>
    ///         <item>
    ///             <description>警告</description>
    ///         </item>
    ///         <item>
    ///             <description>错误</description>
    ///         </item>
    ///         <item>
    ///             <description>文件</description>
    ///         </item>
    ///         <item>
    ///             <description>提示</description>
    ///         </item>
    ///         <item>
    ///             <description>搜索</description>
    ///         </item>
    ///         <item>
    ///             <description>时钟</description>
    ///         </item>
    ///     </list>
    /// </param>
    /// <param name="message">事件信息</param>
    internal static void WriteLine(int level, string message)
    {
        // 获取信息级别对应的 emoji
        var category = GetLevelEmoji(level);

        Debug.WriteLine(message, category);
    }

    /// <summary>
    ///     输出一行事件信息
    /// </summary>
    /// <param name="level">
    ///     <para>信息级别</para>
    ///     <list type="number">
    ///         <item>
    ///             <description>跟踪</description>
    ///         </item>
    ///         <item>
    ///             <description>信息</description>
    ///         </item>
    ///         <item>
    ///             <description>警告</description>
    ///         </item>
    ///         <item>
    ///             <description>错误</description>
    ///         </item>
    ///         <item>
    ///             <description>文件</description>
    ///         </item>
    ///         <item>
    ///             <description>提示</description>
    ///         </item>
    ///         <item>
    ///             <description>搜索</description>
    ///         </item>
    ///         <item>
    ///             <description>时钟</description>
    ///         </item>
    ///     </list>
    /// </param>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void WriteLine(int level, string message, params object?[] args) =>
        WriteLine(level, string.Format(message, args));

    /// <summary>
    ///     输出跟踪级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    internal static void Trace(string message) => WriteLine(1, message);

    /// <summary>
    ///     输出跟踪级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void Trace(string message, params object?[] args) => WriteLine(1, message, args);

    /// <summary>
    ///     输出信息级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    internal static void Info(string message) => WriteLine(2, message);

    /// <summary>
    ///     输出信息级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void Info(string message, params object?[] args) => WriteLine(2, message, args);

    /// <summary>
    ///     输出警告级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    internal static void Warn(string message) => WriteLine(3, message);

    /// <summary>
    ///     输出警告级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void Warn(string message, params object?[] args) => WriteLine(3, message, args);

    /// <summary>
    ///     输出错误级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    internal static void Error(string message) => WriteLine(4, message);

    /// <summary>
    ///     输出错误级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void Error(string message, params object?[] args) => WriteLine(4, message, args);

    /// <summary>
    ///     输出文件级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    internal static void File(string message) => WriteLine(5, message);

    /// <summary>
    ///     输出文件级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void File(string message, params object?[] args) => WriteLine(5, message, args);

    /// <summary>
    ///     输出提示级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    internal static void Tip(string message) => WriteLine(6, message);

    /// <summary>
    ///     输出提示级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void Tip(string message, params object?[] args) => WriteLine(6, message, args);

    /// <summary>
    ///     输出搜索级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    internal static void Search(string message) => WriteLine(7, message);

    /// <summary>
    ///     输出搜索级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void Search(string message, params object?[] args) => WriteLine(7, message, args);

    /// <summary>
    ///     输出时钟级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    internal static void Clock(string message) => WriteLine(8, message);

    /// <summary>
    ///     输出时钟级别事件信息
    /// </summary>
    /// <param name="message">事件信息</param>
    /// <param name="args">格式化参数</param>
    internal static void Clock(string message, params object?[] args) => WriteLine(8, message, args);

    /// <summary>
    ///     获取信息级别对应的 emoji
    /// </summary>
    /// <param name="level">
    ///     <para>信息级别</para>
    ///     <list type="number">
    ///         <item>
    ///             <description>跟踪</description>
    ///         </item>
    ///         <item>
    ///             <description>信息</description>
    ///         </item>
    ///         <item>
    ///             <description>警告</description>
    ///         </item>
    ///         <item>
    ///             <description>错误</description>
    ///         </item>
    ///         <item>
    ///             <description>文件</description>
    ///         </item>
    ///         <item>
    ///             <description>提示</description>
    ///         </item>
    ///         <item>
    ///             <description>搜索</description>
    ///         </item>
    ///         <item>
    ///             <description>时钟</description>
    ///         </item>
    ///     </list>
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string GetLevelEmoji(int level) =>
        level switch
        {
            1 => "🛠️",
            2 => "ℹ️",
            3 => "⚠️",
            4 => "❌",
            5 => "📄",
            6 => "💡",
            7 => "🔍",
            8 => "⏱️",
            _ => string.Empty
        };
}