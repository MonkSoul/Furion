// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.ClayObject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Furion.JsonSerialization;

/// <summary>
/// 解决 Clay 问题
/// </summary>
[SuppressSniffer]
public class NewtonsoftJsonClayJsonConverter : JsonConverter<Clay>
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
    public override Clay ReadJson(JsonReader reader, Type objectType, Clay existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = JValue.ReadFrom(reader).Value<string>();
        return Clay.Parse(value);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, Clay value, JsonSerializer serializer)
    {
        writer.WriteRawValue(value.ToString());
    }
}