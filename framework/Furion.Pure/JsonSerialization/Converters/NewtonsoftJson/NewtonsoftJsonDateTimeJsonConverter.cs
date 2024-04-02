// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Newtonsoft.Json;

namespace Furion.JsonSerialization;

/// <summary>
/// DateTime 类型序列化
/// </summary>
[SuppressSniffer]
public class NewtonsoftJsonDateTimeJsonConverter : JsonConverter<DateTime>
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public NewtonsoftJsonDateTimeJsonConverter()
        : this(default)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    public NewtonsoftJsonDateTimeJsonConverter(string format = "yyyy-MM-dd HH:mm:ss")
    {
        Format = format;
    }

    /// <summary>
    /// 时间格式化格式
    /// </summary>
    public string Format { get; private set; }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="hasExistingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return Penetrates.ConvertToDateTime(ref reader);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    /// <exception cref="NotImplementedException"></exception>
    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString(Format));
    }
}

/// <summary>
/// DateTime 类型序列化
/// </summary>
[SuppressSniffer]
public class NewtonsoftNullableJsonDateTimeJsonConverter : JsonConverter<DateTime?>
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public NewtonsoftNullableJsonDateTimeJsonConverter()
        : this(default)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    public NewtonsoftNullableJsonDateTimeJsonConverter(string format = "yyyy-MM-dd HH:mm:ss")
    {
        Format = format;
    }

    /// <summary>
    /// 时间格式化格式
    /// </summary>
    public string Format { get; private set; }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="hasExistingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return Penetrates.ConvertToDateTime(ref reader);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    /// <exception cref="NotImplementedException"></exception>
    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
    {
        if (value == null) writer.WriteNull();
        else writer.WriteValue(value.Value.ToString(Format));
    }
}