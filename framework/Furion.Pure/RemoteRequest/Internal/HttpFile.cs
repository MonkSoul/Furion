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

namespace Furion.RemoteRequest;

/// <summary>
/// 远程请求文件类
/// </summary>
[SuppressSniffer]
public sealed class HttpFile
{
    /// <summary>
    /// 创建 HttpFile 类
    /// </summary>
    /// <param name="name"></param>
    /// <param name="bytes"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static HttpFile Create(string name, byte[] bytes, string fileName = default)
    {
        return new HttpFile
        {
            Name = name,
            Bytes = bytes,
            FileName = fileName
        };
    }

    /// <summary>
    /// 添加多个文件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static HttpFile[] CreateMultiple(string name, params (byte[] bytes, string fileName)[] items)
    {
        var files = new List<HttpFile>();
        if (items == null || items.Length == 0) return files.ToArray();

        foreach (var (bytes, fileName) in items)
        {
            files.Add(Create(name, bytes, fileName));
        }

        return files.ToArray();
    }

    /// <summary>
    /// 表单名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件字节数组
    /// </summary>
    public byte[] Bytes { get; set; }
}
