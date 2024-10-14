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