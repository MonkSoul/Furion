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

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Furion.Logging;

/// <summary>
/// 解决 LoggingMonitor 特别字段序列化过大问题
/// </summary>
internal sealed class IgnorePropertiesContractResolver : CamelCasePropertyNamesContractResolver
{
    /// <summary>
    /// 被忽略的属性名称
    /// </summary>
    private readonly string[] _names;

    /// <summary>
    /// 被忽略的属性类型
    /// </summary>
    private readonly Type[] _type;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="names"></param>
    /// <param name="types"></param>
    public IgnorePropertiesContractResolver(string[] names, Type[] types)
    {
        _names = names ?? Array.Empty<string>();
        _type = types ?? Array.Empty<Type>();
    }

    /// <summary>
    /// 重写需要序列化的属性名
    /// </summary>
    /// <param name="type"></param>
    /// <param name="memberSerialization"></param>
    /// <returns></returns>
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var allProperties = base.CreateProperties(type, memberSerialization);

        return allProperties.Where(p =>
                !_names.Contains(p.PropertyName, StringComparer.OrdinalIgnoreCase)
                && !_type.Contains(p.PropertyType)).ToList();
    }
}