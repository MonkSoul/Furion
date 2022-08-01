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
/// 视图引擎接口
/// </summary>
public interface IViewEngine
{
    /// <summary>
    /// 编译并运行
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    string RunCompile(string content, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null);

    /// <summary>
    /// 编译并运行
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    Task<string> RunCompileAsync(string content, object model = null, Action<IViewEngineOptionsBuilder> builderAction = null);

    /// <summary>
    /// 编译并运行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    string RunCompile<T>(string content, T model, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : class, new();

    /// <summary>
    /// 编译并运行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    Task<string> RunCompileAsync<T>(string content, T model, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : class, new();

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="cacheFileName"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    string RunCompileFromCached(string content, object model = null, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null);

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="cacheFileName"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    Task<string> RunCompileFromCachedAsync(string content, object model = null, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null);

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="cacheFileName"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    string RunCompileFromCached<T>(string content, T model, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : class, new();

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="model"></param>
    /// <param name="cacheFileName"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    Task<string> RunCompileFromCachedAsync<T>(string content, T model, string cacheFileName = default, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : class, new();

    /// <summary>
    /// 编译模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    IViewEngineTemplate Compile(string content, Action<IViewEngineOptionsBuilder> builderAction = null);

    /// <summary>
    /// 编译模板
    /// </summary>
    /// <param name="content"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    Task<IViewEngineTemplate> CompileAsync(string content, Action<IViewEngineOptionsBuilder> builderAction = null);

    /// <summary>
    /// 编译模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    IViewEngineTemplate<T> Compile<T>(string content, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : IViewEngineModel;

    /// <summary>
    /// 编译模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="builderAction"></param>
    /// <returns></returns>
    Task<IViewEngineTemplate<T>> CompileAsync<T>(string content, Action<IViewEngineOptionsBuilder> builderAction = null)
        where T : IViewEngineModel;
}