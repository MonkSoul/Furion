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

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Furion.Schedule;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj">对象</param>
    /// <returns><see cref="string"/></returns>
    internal static string Serialize(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    /// <summary>
    /// 反序列化对象
    /// </summary>
    /// <param name="json">JSON 字符串</param>
    /// <returns>T</returns>
    internal static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }

    /// <summary>
    /// 将属性名切割成多个单词
    /// </summary>
    /// <param name="propertyName">属性名</param>
    /// <returns>单词数组</returns>
    internal static string[] SplitToWords(string propertyName)
    {
        if (propertyName == null) return Array.Empty<string>();

        if (string.IsNullOrWhiteSpace(propertyName)) return new string[] { propertyName };
        if (propertyName.Length == 1) return new string[] { propertyName };

        return Regex.Split(propertyName, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
            .Where(u => u.Length > 0)
            .ToArray();
    }

    /// <summary>
    /// 设置字符串首字母大/小写
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="isUpper">是否大写</param>
    /// <returns><see cref="string"/></returns>
    internal static string SetFirstLetterCase(string str, bool isUpper = true)
    {
        if (string.IsNullOrWhiteSpace(str)) return str;

        var firstLetter = str.First().ToString();
        return string.Concat(isUpper
            ? firstLetter.ToUpper()
            : firstLetter.ToLower(), str.AsSpan(1));
    }

    /// <summary>
    /// 根据属性名获取指定的命名法
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="naming"></param>
    /// <returns></returns>
    internal static string GetNaming(string propertyName, NamingConventions naming = NamingConventions.Pascal)
    {
        var words = SplitToWords(propertyName);
        var tempWords = new List<string>();

        foreach (var word in words)
        {
            switch (naming)
            {
                case NamingConventions.CamelCase:
                    tempWords.Add(SetFirstLetterCase(word));
                    continue;
                case NamingConventions.Pascal:
                case NamingConventions.UnderScoreCase:
                    tempWords.Add(SetFirstLetterCase(word, false));
                    continue;
            }
        }

        return string.Join(naming == NamingConventions.UnderScoreCase ? "_" : string.Empty, tempWords);
    }

    /// <summary>
    /// 获取 SQL 的值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    internal static string GetSqlValueOrNull(object obj)
    {
        return obj == null ? "NULL" : $"'{obj}'";
    }

    /// <summary>
    /// 高性能创建 JSON 字符串
    /// </summary>
    /// <param name="writeAction"></param>
    /// <returns></returns>
    internal static string Write(Action<Utf8JsonWriter> writeAction)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
        {
            Indented = true,
            // 解决中文乱码问题
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

        writeAction?.Invoke(writer);

        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}