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

using Furion.JsonSerialization;

namespace Newtonsoft.Json;

/// <summary>
/// Newtonsoft.Json 拓展
/// </summary>
[SuppressSniffer]
public static class NewtonsoftJsonExtensions
{
    /// <summary>
    /// 添加 long/long? 类型序列化处理
    /// </summary>
    /// <remarks></remarks>
    public static IList<JsonConverter> AddLongTypeConverters(this IList<JsonConverter> converters)
    {
        converters.Add(new NewtonsoftJsonLongToStringJsonConverter());
        converters.Add(new NewtonsoftJsonNullableLongToStringJsonConverter());

        return converters;
    }

    /// <summary>
    /// 添加 DateOnly/DateOnly? 类型序列化处理
    /// </summary>
    /// <param name="converters"></param>
    /// <returns></returns>
    public static IList<JsonConverter> AddDateOnlyConverters(this IList<JsonConverter> converters)
    {
#if !NET5_0
        converters.Add(new NewtonsoftJsonDateOnlyJsonConverter());
        converters.Add(new NewtonsoftJsonNullableDateOnlyJsonConverter());
#endif
        return converters;
    }

    /// <summary>
    /// 添加 TimeOnly/TimeOnly? 类型序列化处理
    /// </summary>
    /// <param name="converters"></param>
    /// <returns></returns>
    public static IList<JsonConverter> AddTimeOnlyConverters(this IList<JsonConverter> converters)
    {
#if !NET5_0
        converters.Add(new NewtonsoftJsonTimeOnlyJsonConverter());
        converters.Add(new NewtonsoftJsonNullableTimeOnlyJsonConverter());
#endif
        return converters;
    }
}