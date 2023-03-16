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

#if !NET5_0
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Furion.JsonSerialization;

/// <summary>
/// DateOnly 类型序列化
/// </summary>
[SuppressSniffer]
public class NewtonsoftJsonDateOnlyJsonConverter : JsonConverter<DateOnly>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public NewtonsoftJsonDateOnlyJsonConverter()
    {
        Format ??= "yyyy-MM-dd";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    public NewtonsoftJsonDateOnlyJsonConverter(string format)
    {
        Format = format;
    }

    /// <summary>
    /// 日期格式化格式
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
    public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = JValue.ReadFrom(reader).Value<string>();
        return DateOnly.Parse(value);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value.ToString(Format));
    }
}

/// <summary>
/// DateOnly? 类型序列化
/// </summary>
[SuppressSniffer]
public class NewtonsoftJsonNullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public NewtonsoftJsonNullableDateOnlyJsonConverter()
    {
        Format ??= "yyyy-MM-dd";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    public NewtonsoftJsonNullableDateOnlyJsonConverter(string format)
    {
        Format = format;
    }

    /// <summary>
    /// 日期格式化格式
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
    public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = JValue.ReadFrom(reader).Value<string>();
        return !string.IsNullOrWhiteSpace(value) ? DateOnly.Parse(value) : null;
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
    {
        if (value == null) writer.WriteNull();
        else serializer.Serialize(writer, value.Value.ToString(Format));
    }
}
#endif