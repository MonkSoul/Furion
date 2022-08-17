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

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎模板模型实现类
/// </summary>
[SuppressSniffer]
public abstract class ViewEngineModel : IViewEngineModel
{
    /// <summary>
    /// 字符串构建器
    /// </summary>
    private readonly StringBuilder stringBuilder = new();

    /// <summary>
    /// 特性前缀
    /// </summary>
    private string attributeSuffix = null;

    /// <summary>
    /// 模型
    /// </summary>
    public dynamic Model { get; set; }

    /// <summary>
    /// 写入字面量
    /// </summary>
    /// <param name="literal"></param>
    public void WriteLiteral(string literal = null)
    {
        WriteLiteralAsync(literal).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 写入字面量
    /// </summary>
    /// <param name="literal"></param>
    /// <returns></returns>
    public virtual Task WriteLiteralAsync(string literal = null)
    {
        stringBuilder.Append(literal);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 写入对象
    /// </summary>
    /// <param name="obj"></param>
    public void Write(object obj = null)
    {
        WriteAsync(obj).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 写入对象
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public virtual Task WriteAsync(object obj = null)
    {
        stringBuilder.Append(obj);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 写入特性
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="suffix"></param>
    /// <param name="suffixOffset"></param>
    /// <param name="attributeValuesCount"></param>
    public void BeginWriteAttribute(string name, string prefix, int prefixOffset, string suffix, int suffixOffset,
        int attributeValuesCount)
    {
        BeginWriteAttributeAsync(name, prefix, prefixOffset, suffix, suffixOffset, attributeValuesCount).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 写入特性
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="suffix"></param>
    /// <param name="suffixOffset"></param>
    /// <param name="attributeValuesCount"></param>
    /// <returns></returns>
    public virtual Task BeginWriteAttributeAsync(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount)
    {
        attributeSuffix = suffix;
        stringBuilder.Append(prefix);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 写入特性值
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="value"></param>
    /// <param name="valueOffset"></param>
    /// <param name="valueLength"></param>
    /// <param name="isLiteral"></param>
    public void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength,
        bool isLiteral)
    {
        WriteAttributeValueAsync(prefix, prefixOffset, value, valueOffset, valueLength, isLiteral).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 写入特性值
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="value"></param>
    /// <param name="valueOffset"></param>
    /// <param name="valueLength"></param>
    /// <param name="isLiteral"></param>
    /// <returns></returns>
    public virtual Task WriteAttributeValueAsync(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral)
    {
        stringBuilder.Append(prefix);
        stringBuilder.Append(value);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 结束写入特性
    /// </summary>
    public void EndWriteAttribute()
    {
        EndWriteAttributeAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// 结束写入特性
    /// </summary>
    /// <returns></returns>
    public virtual Task EndWriteAttributeAsync()
    {
        stringBuilder.Append(attributeSuffix);
        attributeSuffix = null;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 执行
    /// </summary>
    public void Execute()
    {
        ExecuteAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <returns></returns>
    public virtual Task ExecuteAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <returns></returns>
    public virtual string Result()
    {
        return ResultAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <returns></returns>
    public virtual Task<string> ResultAsync()
    {
        return Task.FromResult<string>(stringBuilder.ToString());
    }
}

/// <summary>
/// 视图引擎模板模型实现类
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressSniffer]
public abstract class ViewEngineModel<T> : ViewEngineModel
{
    /// <summary>
    /// 强类型
    /// </summary>
    public new T Model { get; set; }
}