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
/// 视图引擎模板（编译后）
/// </summary>
public interface IViewEngineTemplate
{
    /// <summary>
    /// 保存到流中
    /// </summary>
    /// <param name="stream"></param>
    void SaveToStream(Stream stream);

    /// <summary>
    /// 保存到流中
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Task SaveToStreamAsync(Stream stream);

    /// <summary>
    /// 保存到文件
    /// </summary>
    /// <param name="fileName"></param>
    void SaveToFile(string fileName);

    /// <summary>
    /// 保存到文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    Task SaveToFileAsync(string fileName);

    /// <summary>
    /// 执行编译
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    string Run(object model = null);

    /// <summary>
    /// 执行编译
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<string> RunAsync(object model = null);
}

/// <summary>
/// 泛型视图编译模板接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IViewEngineTemplate<out T>
    where T : IViewEngineModel
{
    /// <summary>
    /// 保存到流中
    /// </summary>
    /// <param name="stream"></param>
    void SaveToStream(Stream stream);

    /// <summary>
    /// 保存到流中
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    Task SaveToStreamAsync(Stream stream);

    /// <summary>
    /// 保存到文件
    /// </summary>
    /// <param name="fileName"></param>
    void SaveToFile(string fileName);

    /// <summary>
    /// 保存到文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    Task SaveToFileAsync(string fileName);

    /// <summary>
    /// 执行编译
    /// </summary>
    /// <param name="initializer"></param>
    /// <returns></returns>
    string Run(Action<T> initializer);

    /// <summary>
    /// 执行编译
    /// </summary>
    /// <param name="initializer"></param>
    /// <returns></returns>
    Task<string> RunAsync(Action<T> initializer);
}