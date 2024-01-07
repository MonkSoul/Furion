// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Furion.JsonSerialization;

/// <summary>
/// DateTimeOffset 类型序列化
/// </summary>
[SuppressSniffer]
public class SystemTextJsonDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SystemTextJsonDateTimeOffsetJsonConverter()
    {
        Format ??= "yyyy-MM-dd HH:mm:ss";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    public SystemTextJsonDateTimeOffsetJsonConverter(string format)
    {
        Format = format;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    /// <param name="outputToLocalDateTime"></param>
    public SystemTextJsonDateTimeOffsetJsonConverter(string format, bool outputToLocalDateTime)
    {
        Format = format;
        Localized = outputToLocalDateTime;
    }

    /// <summary>
    /// 时间格式化格式
    /// </summary>
    public string Format { get; private set; }

    /// <summary>
    /// 是否输出为为当地时间
    /// </summary>
    public bool Localized { get; private set; } = false;

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.SpecifyKind(Penetrates.ConvertToDateTime(ref reader), Localized ? DateTimeKind.Local : DateTimeKind.Utc);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        // 判断是否序列化成当地时间
        var formatDateTime = Localized ? value.ConvertToDateTime() : value;
        writer.WriteStringValue(formatDateTime.ToString(Format));
    }
}

/// <summary>
/// DateTimeOffset? 类型序列化
/// </summary>
[SuppressSniffer]
public class SystemTextJsonNullableDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset?>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SystemTextJsonNullableDateTimeOffsetJsonConverter()
    {
        Format ??= "yyyy-MM-dd HH:mm:ss";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    public SystemTextJsonNullableDateTimeOffsetJsonConverter(string format)
    {
        Format = format;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    /// <param name="outputToLocalDateTime"></param>
    public SystemTextJsonNullableDateTimeOffsetJsonConverter(string format, bool outputToLocalDateTime)
    {
        Format = format;
        Localized = outputToLocalDateTime;
    }

    /// <summary>
    /// 时间格式化格式
    /// </summary>
    public string Format { get; private set; }

    /// <summary>
    /// 是否输出为为当地时间
    /// </summary>
    public bool Localized { get; private set; } = false;

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.SpecifyKind(Penetrates.ConvertToDateTime(ref reader), Localized ? DateTimeKind.Local : DateTimeKind.Utc);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value == null) writer.WriteNullValue();
        else
        {
            // 判断是否序列化成当地时间
            var formatDateTime = Localized ? value.ConvertToDateTime() : value;
            writer.WriteStringValue(formatDateTime.Value.ToString(Format));
        }
    }
}