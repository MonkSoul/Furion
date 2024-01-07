// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Furion.Logging;

/// <summary>
/// <see cref="LogMessage"/> 拓展
/// </summary>
[SuppressSniffer]
public static class LogMessageExtensions
{
    /// <summary>
    /// 高性能创建 JSON 对象字符串
    /// </summary>
    /// <param name="_"><see cref="LogMessage"/></param>
    /// <param name="writeAction"></param>
    /// <param name="writeIndented">是否对 JSON 格式化</param>
    /// <returns><see cref="string"/></returns>
    public static string Write(this LogMessage _, Action<Utf8JsonWriter> writeAction, bool writeIndented = false)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            // 解决中文乱码问题
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = writeIndented
        });

        writeAction?.Invoke(writer);

        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    /// <summary>
    /// 高性能创建 JSON 数组字符串
    /// </summary>
    /// <param name="logMsg"><see cref="LogMessage"/></param>
    /// <param name="writeAction"></param>
    /// <param name="writeIndented">是否对 JSON 格式化</param>
    /// <returns><see cref="string"/></returns>
    public static string WriteArray(this LogMessage logMsg, Action<Utf8JsonWriter> writeAction, bool writeIndented = false)
    {
        return logMsg.Write(writer =>
        {
            writer.WriteStartArray();

            writeAction?.Invoke(writer);

            writer.WriteEndArray();
        }, writeIndented);
    }
}