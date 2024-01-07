// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Furion.JsonSerialization;

/// <summary>
/// 解决 long 精度问题
/// </summary>
[SuppressSniffer]
public class NewtonsoftJsonLongToStringJsonConverter : JsonConverter<long>
{
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="hasExistingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jt = JValue.ReadFrom(reader);
        return jt.Value<long>();
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value.ToString());
    }
}

/// <summary>
/// 解决 long? 精度问题
/// </summary>
[SuppressSniffer]
public class NewtonsoftJsonNullableLongToStringJsonConverter : JsonConverter<long?>
{
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="hasExistingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    public override long? ReadJson(JsonReader reader, Type objectType, long? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jt = JValue.ReadFrom(reader);
        return jt.Value<long?>();
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, long? value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value?.ToString());
    }
}