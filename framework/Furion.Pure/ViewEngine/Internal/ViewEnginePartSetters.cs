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

namespace Furion.ViewEngine;

/// <summary>
/// 字符串模板执行部件
/// </summary>
public sealed partial class ViewEnginePart
{
    /// <summary>
    /// 设置模板
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplate(string template)
    {
        if (!string.IsNullOrWhiteSpace(template)) Template = template;
        return this;
    }

    /// <summary>
    /// 设置模板数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplateModel<T>(T model)
        where T : class, new()
    {
        TemplateModel = (typeof(T), model);
        return this;
    }

    /// <summary>
    /// 设置模板数据
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplateModel(object model)
    {
        return SetTemplateModel<object>(model);
    }

    /// <summary>
    /// 设置模板构建选项
    /// </summary>
    /// <param name="optionsBuilder"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplateOptionsBuilder(Action<IViewEngineOptionsBuilder> optionsBuilder = default)
    {
        if (optionsBuilder != null) TemplateOptionsBuilder = optionsBuilder;
        return this;
    }

    /// <summary>
    /// 设置模板缓存文件名（不含拓展名）
    /// </summary>
    /// <param name="cachedFileName"></param>
    /// <returns></returns>
    public ViewEnginePart SetTemplateCachedFileName(string cachedFileName)
    {
        if (!string.IsNullOrWhiteSpace(cachedFileName)) TemplateCachedFileName = cachedFileName;
        return this;
    }

    /// <summary>
    /// 视图模板服务作用域
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public ViewEnginePart SetViewEngineScoped(IServiceProvider serviceProvider)
    {
        if (serviceProvider != null) ViewEngineScoped = serviceProvider;
        return this;
    }
}