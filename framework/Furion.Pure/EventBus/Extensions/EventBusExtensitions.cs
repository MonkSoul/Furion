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

using System.Reflection;

namespace Furion.Extensitions.EventBus;

/// <summary>
/// 事件总线拓展类
/// </summary>
[SuppressSniffer]
public static class EventBusExtensitions
{
    /// <summary>
    /// 将事件枚举 Id 转换成字符串对象
    /// </summary>
    /// <param name="em"></param>
    /// <returns></returns>
    public static string ParseToString(this Enum em)
    {
        var enumType = em.GetType();
        return $"{enumType.Assembly.GetName().Name};{enumType.FullName}.{em}";
    }

    /// <summary>
    /// 将事件枚举字符串转换成枚举对象
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Enum ParseToEnum(this string str)
    {
        var assemblyName = str[..str.IndexOf(';')];
        var fullName = str[(str.IndexOf(';') + 1)..str.LastIndexOf('.')];
        var name = str[(str.LastIndexOf('.') + 1)..];

        return Enum.Parse(Assembly.Load(assemblyName).GetType(fullName), name) as Enum;
    }
}