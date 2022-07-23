// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Furion.Logging;

/// <summary>
/// <see cref="FileLogMessage"/> 拓展
/// </summary>
[SuppressSniffer]
public static class FileLogMessageExtensions
{
    /// <summary>
    /// 高性能写入日志模板
    /// </summary>
    /// <param name="_"><see cref="FileLogMessage"/></param>
    /// <param name="writeAction"></param>
    /// <returns></returns>
    public static string Write(this FileLogMessage _, Action<Utf8JsonWriter> writeAction)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            // 解决中文乱码问题
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        writeAction?.Invoke(writer);

        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    /// <summary>
    /// 高性能写入数组日志模板
    /// </summary>
    /// <param name="logMsg"><see cref="FileLogMessage"/></param>
    /// <param name="writeAction"></param>
    /// <returns></returns>
    public static string WriteArray(this FileLogMessage logMsg, Action<Utf8JsonWriter> writeAction)
    {
        return logMsg.Write(writer =>
        {
            writer.WriteStartArray();

            writeAction?.Invoke(writer);

            writer.WriteEndArray();
        });
    }
}