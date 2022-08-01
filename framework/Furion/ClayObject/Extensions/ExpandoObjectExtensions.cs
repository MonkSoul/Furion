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

using Furion.Extensions;
using System.Dynamic;
using System.Text.Json;

namespace Furion.ClayObject.Extensions;

/// <summary>
/// ExpandoObject 对象拓展
/// </summary>
[SuppressSniffer]
public static class ExpandoObjectExtensions
{
    /// <summary>
    /// 将对象转 ExpandoObject 类型
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ExpandoObject ToExpandoObject(this object value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value));

        if (value is Clay clay && clay.IsObject)
        {
            dynamic clayExpando = new ExpandoObject();
            var dic = (IDictionary<string, object>)clayExpando;

            foreach (KeyValuePair<string, dynamic> item in (dynamic)clay)
            {
                dic.Add(item.Key, item.Value is Clay v ? v.ToExpandoObject() : item.Value);
            }

            return clayExpando;
        }

        if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
        {
            dynamic clayExpando = new ExpandoObject();
            var dic = (IDictionary<string, object>)clayExpando;
            var objDic = jsonElement.ToObject() as IDictionary<string, object>;

            foreach (var item in objDic)
            {
                dic.Add(item);
            }

            return clayExpando;
        }

        if (value is not ExpandoObject expando)
        {
            expando = new ExpandoObject();
            var dict = (IDictionary<string, object>)expando;

            var dictionary = value.ToDictionary();
            foreach (var kvp in dictionary)
            {
                dict.Add(kvp);
            }
        }

        return expando;
    }

    /// <summary>
    /// 移除 ExpandoObject 对象属性
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <param name="propertyName"></param>
    public static void RemoveProperty(this ExpandoObject expandoObject, string propertyName)
    {
        if (expandoObject == null)
            throw new ArgumentNullException(nameof(expandoObject));

        if (propertyName == null)
            throw new ArgumentNullException(nameof(propertyName));

        ((IDictionary<string, object>)expandoObject).Remove(propertyName);
    }

    /// <summary>
    /// 判断 ExpandoObject 是否为空
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <returns></returns>
    public static bool Empty(this ExpandoObject expandoObject)
    {
        return !((IDictionary<string, object>)expandoObject).Any();
    }

    /// <summary>
    /// 判断 ExpandoObject 是否拥有某属性
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static bool HasProperty(this ExpandoObject expandoObject, string propertyName)
    {
        if (expandoObject == null)
            throw new ArgumentNullException(nameof(expandoObject));

        if (propertyName == null)
            throw new ArgumentNullException(nameof(propertyName));

        return ((IDictionary<string, object>)expandoObject).ContainsKey(propertyName);
    }

    /// <summary>
    /// 实现 ExpandoObject 浅拷贝
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <returns></returns>
    public static ExpandoObject ShallowCopy(this ExpandoObject expandoObject)
    {
        return Copy(expandoObject, false);
    }

    /// <summary>
    /// 实现 ExpandoObject 深度拷贝
    /// </summary>
    /// <param name="expandoObject"></param>
    /// <returns></returns>
    public static ExpandoObject DeepCopy(this ExpandoObject expandoObject)
    {
        return Copy(expandoObject, true);
    }

    /// <summary>
    /// 拷贝 ExpandoObject 对象
    /// </summary>
    /// <param name="original"></param>
    /// <param name="deep"></param>
    /// <returns></returns>
    private static ExpandoObject Copy(ExpandoObject original, bool deep)
    {
        var clone = new ExpandoObject();

        var _original = (IDictionary<string, object>)original;
        var _clone = (IDictionary<string, object>)clone;

        foreach (var kvp in _original)
        {
            _clone.Add(
                kvp.Key,
                deep && kvp.Value is ExpandoObject eObject ? DeepCopy(eObject) : kvp.Value
            );
        }

        return clone;
    }
}