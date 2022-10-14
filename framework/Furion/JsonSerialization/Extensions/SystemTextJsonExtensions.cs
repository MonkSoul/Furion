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
using System.Text.Json.Serialization;

namespace System.Text.Json;

/// <summary>
/// System.Text.Json 拓展
/// </summary>
[SuppressSniffer]
public static class SystemTextJsonExtensions
{
    /// <summary>
    /// 添加 DateTime/DateTime?/DateTimeOffset/DateTimeOffset? 类型序列化处理
    /// </summary>
    /// <param name="converters"></param>
    /// <param name="outputFormat"></param>
    /// <param name="localized">自动转换 DateTimeOffset 为当地时间</param>
    /// <returns></returns>
    public static IList<JsonConverter> AddDateTimeTypeConverters(this IList<JsonConverter> converters, string outputFormat = default, bool localized = false)
    {
        converters.Add(new SystemTextJsonDateTimeJsonConverter(outputFormat));
        converters.Add(new SystemTextJsonNullableDateTimeJsonConverter(outputFormat));

        converters.Add(new SystemTextJsonDateTimeOffsetJsonConverter(outputFormat, localized));
        converters.Add(new SystemTextJsonNullableDateTimeOffsetJsonConverter(outputFormat, localized));

        return converters;
    }

    /// <summary>
    /// 添加 long/long? 类型序列化处理
    /// </summary>
    /// <remarks></remarks>
    public static IList<JsonConverter> AddLongTypeConverters(this IList<JsonConverter> converters)
    {
        converters.Add(new SystemTextJsonLongToStringJsonConverter());
        converters.Add(new SystemTextJsonNullableLongToStringJsonConverter());

        return converters;
    }
}