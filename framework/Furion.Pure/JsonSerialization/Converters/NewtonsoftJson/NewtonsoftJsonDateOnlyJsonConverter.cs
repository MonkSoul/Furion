// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#if !NET5_0
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Furion.JsonSerialization;
/// <summary>
/// DateOnly 类型序列化
/// </summary>
public class NewtonsoftJsonDateOnlyJsonConverter : JsonConverter<DateOnly>
{
    /// <summary>
    /// 
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
public class NewtonsoftJsonNullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
{
    /// <summary>
    /// 
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