// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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

namespace Furion.JsonSerialization;

/// <summary>
/// JSON 静态帮助类
/// </summary>
[SuppressSniffer]
public static class JSON
{
    /// <summary>
    /// 获取 JSON 序列化提供器
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IJsonSerializerProvider GetJsonSerializer(IServiceProvider serviceProvider = default)
    {
        return App.GetService<IJsonSerializerProvider>(serviceProvider ?? App.RootServices);
    }

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="value"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static string Serialize(object value, object jsonSerializerOptions = default, IServiceProvider serviceProvider = default)
    {
        return GetJsonSerializer(serviceProvider).Serialize(value, jsonSerializerOptions);
    }

    /// <summary>
    /// 反序列化字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <param name="jsonSerializerOptions"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static T Deserialize<T>(string json, object jsonSerializerOptions = default, IServiceProvider serviceProvider = default)
    {
        return GetJsonSerializer(serviceProvider).Deserialize<T>(json, jsonSerializerOptions);
    }

    /// <summary>
    /// 获取 JSON 配置选项
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static TOptions GetSerializerOptions<TOptions>(IServiceProvider serviceProvider = default)
        where TOptions : class
    {
        return GetJsonSerializer(serviceProvider).GetSerializerOptions() as TOptions;
    }
}