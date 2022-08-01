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

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎模板模型接口
/// </summary>
public interface IViewEngineModel
{
    /// <summary>
    /// 模型
    /// </summary>
    dynamic Model { get; set; }

    /// <summary>
    /// 写入字面量
    /// </summary>
    /// <param name="literal"></param>
    void WriteLiteral(string literal = null);

    /// <summary>
    /// 写入字面量
    /// </summary>
    /// <param name="literal"></param>
    /// <returns></returns>
    Task WriteLiteralAsync(string literal = null);

    /// <summary>
    /// 写入对象
    /// </summary>
    /// <param name="obj"></param>
    void Write(object obj = null);

    /// <summary>
    /// 写入对象
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    Task WriteAsync(object obj = null);

    /// <summary>
    /// 开始写入特性
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="suffix"></param>
    /// <param name="suffixOffset"></param>
    /// <param name="attributeValuesCount"></param>
    void BeginWriteAttribute(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount);

    /// <summary>
    /// 开始写入特性
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="suffix"></param>
    /// <param name="suffixOffset"></param>
    /// <param name="attributeValuesCount"></param>
    /// <returns></returns>
    Task BeginWriteAttributeAsync(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount);

    /// <summary>
    /// 写入特性值
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="value"></param>
    /// <param name="valueOffset"></param>
    /// <param name="valueLength"></param>
    /// <param name="isLiteral"></param>
    void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral);

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
    Task WriteAttributeValueAsync(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral);

    /// <summary>
    /// 结束写入特性
    /// </summary>
    void EndWriteAttribute();

    /// <summary>
    /// 结束写入特性
    /// </summary>
    /// <returns></returns>
    Task EndWriteAttributeAsync();

    /// <summary>
    /// 执行
    /// </summary>
    void Execute();

    /// <summary>
    /// 执行
    /// </summary>
    /// <returns></returns>
    Task ExecuteAsync();

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <returns></returns>
    string Result();

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <returns></returns>
    Task<string> ResultAsync();
}