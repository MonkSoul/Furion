// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Furion.Schedule;

/// <summary>
/// DateTime 类型序列化/反序列化处理
/// </summary>
internal sealed class DateTimeJsonConverter : JsonConverter<DateTime>
{
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"><see cref="Utf8JsonReader"/></param>
    /// <param name="typeToConvert">需要转换的类型</param>
    /// <param name="options">序列化配置选项</param>
    /// <returns><see cref="DateTime"/></returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Convert.ToDateTime(reader.GetString());
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"><see cref="Utf8JsonWriter"/></param>
    /// <param name="value"><see cref="DateTime"/></param>
    /// <param name="options">序列化配置选项</param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}