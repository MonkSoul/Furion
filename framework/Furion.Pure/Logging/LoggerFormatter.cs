// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.Logging;
using System.Text.Json;

namespace System;

/// <summary>
/// 日志格式化静态类
/// </summary>
[SuppressSniffer]
public static class LoggerFormatter
{
    /// <summary>
    /// Json 输出格式化
    /// </summary>
    public static readonly Func<LogMessage, string> Json = (logMsg) =>
    {
        return logMsg.Write(writer => WriteJson(logMsg, writer));
    };

    /// <summary>
    /// Json 输出格式化
    /// </summary>
    public static readonly Func<LogMessage, string> JsonIndented = (logMsg) =>
    {
        return logMsg.Write(writer => WriteJson(logMsg, writer), true);
    };

    /// <summary>
    /// 写入 JSON
    /// </summary>
    /// <param name="logMsg"></param>
    /// <param name="writer"></param>
    private static void WriteJson(LogMessage logMsg, Utf8JsonWriter writer)
    {
        writer.WriteStartObject();

        // 输出日志级别
        writer.WriteString("logLevel", logMsg.LogLevel.ToString());

        // 输出日志时间
        writer.WriteString("logDateTime", logMsg.LogDateTime.ToString("o"));

        // 输出日志类别
        writer.WriteString("logName", logMsg.LogName);

        // 输出日志事件 Id
        writer.WriteNumber("eventId", logMsg.EventId.Id);

        // 输出日志消息
        writer.WriteString("message", logMsg.Message);

        // 输出日志所在线程 Id
        writer.WriteNumber("threadId", logMsg.ThreadId);

        // 输出是否使用 UTC 时间戳
        writer.WriteBoolean("useUtcTimestamp", logMsg.UseUtcTimestamp);

        // 输出请求 TraceId
        writer.WriteString("traceId", logMsg.TraceId);

        // 输出异常信息
        writer.WritePropertyName("exception");
        if (logMsg.Exception == null) writer.WriteNullValue();
        else writer.WriteStringValue(logMsg.Exception.ToString());

        writer.WriteEndObject();
    }
}