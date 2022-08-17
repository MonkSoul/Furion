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

using System.Reflection;

namespace Furion.ViewEngine;

/// <summary>
/// 字符串模板执行部件
/// </summary>
public sealed partial class ViewEnginePart
{
    /// <summary>
    /// 编译并运行
    /// </summary>
    public string RunCompile()
    {
        return InvokeRunCompileMethod(nameof(IViewEngine.RunCompile)) as string;
    }

    /// <summary>
    /// 编译并运行
    /// </summary>
    public Task<string> RunCompileAsync()
    {
        return InvokeRunCompileMethod(nameof(IViewEngine.RunCompileAsync)) as Task<string>;
    }

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    public string RunCompileFromCached()
    {
        return InvokeRunCompileMethod(nameof(IViewEngine.RunCompileFromCached), true) as string;
    }

    /// <summary>
    /// 通过缓存解析模板
    /// </summary>
    public Task<string> RunCompileFromCachedAsync()
    {
        return InvokeRunCompileMethod(nameof(IViewEngine.RunCompileFromCachedAsync), true) as Task<string>;
    }

    /// <summary>
    /// 执行模板方法
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="isCached"></param>
    /// <returns></returns>
    private object InvokeRunCompileMethod(string methodName, bool isCached = false)
    {
        var viewEngine = GetViewEngine();
        var viewEngineType = viewEngine.GetType();

        // 反射获取视图引擎方法
        var runCompileMethod = TemplateModel.Type == typeof(object)
            ? viewEngineType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                       .First(m => m.Name == methodName && !m.IsGenericMethod)
            : viewEngineType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                       .First(m => m.Name == methodName && m.IsGenericMethod)
                       .MakeGenericMethod(TemplateModel.Type);

        return !isCached
            ? runCompileMethod.Invoke(viewEngine, new object[] {
                    Template,TemplateModel.Model,TemplateOptionsBuilder
            })
            : runCompileMethod.Invoke(viewEngine, new object[] {
                    Template,TemplateModel.Model,TemplateCachedFileName, TemplateOptionsBuilder
            });
    }

    /// <summary>
    /// 获取视图引擎对象
    /// </summary>
    /// <returns></returns>
    private IViewEngine GetViewEngine()
    {
        return App.GetService<IViewEngine>(ViewEngineScoped ?? App.RootServices)
            ?? throw new InvalidOperationException("Please confirm whether the view engine is registered successfully.");
    }
}