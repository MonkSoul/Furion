// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.JsonSerialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Encodings.Web;

namespace Furion.Logging;

/// <summary>
/// 解决 JsonElement 问题
/// </summary>
[SuppressSniffer]
public class JsonElementConverter : JsonConverter<System.Text.Json.JsonElement>
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
    public override System.Text.Json.JsonElement ReadJson(JsonReader reader, Type objectType, System.Text.Json.JsonElement existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = JValue.ReadFrom(reader).Value<string>();
        return (System.Text.Json.JsonElement)System.Text.Json.JsonSerializer.Deserialize<object>(value);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, System.Text.Json.JsonElement value, JsonSerializer serializer)
    {
        var jsonSerializerOptions = new System.Text.Json.JsonSerializerOptions()
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        jsonSerializerOptions.Converters.Add(new SystemTextJsonLongToStringJsonConverter());
        jsonSerializerOptions.Converters.Add(new SystemTextJsonNullableLongToStringJsonConverter());

        serializer.Serialize(writer, System.Text.Json.JsonSerializer.Serialize(value, jsonSerializerOptions));
    }
}