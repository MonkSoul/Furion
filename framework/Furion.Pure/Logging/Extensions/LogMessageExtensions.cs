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