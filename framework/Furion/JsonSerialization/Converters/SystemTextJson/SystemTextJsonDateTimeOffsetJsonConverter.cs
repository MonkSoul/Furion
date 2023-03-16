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
        return DateTime.SpecifyKind(Convert.ToDateTime(reader.GetString()), Localized ? DateTimeKind.Local : DateTimeKind.Utc);
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
        return DateTime.SpecifyKind(Convert.ToDateTime(reader.GetString()), Localized ? DateTimeKind.Local : DateTimeKind.Utc);
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