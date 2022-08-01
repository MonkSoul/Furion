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
/// 字符串模板执行部件
/// </summary>
[SuppressSniffer]
public sealed partial class ViewEnginePart
{
    /// <summary>
    /// 静态缺省 视图 部件
    /// </summary>
    public static ViewEnginePart Default => new();

    /// <summary>
    /// 字符串模板
    /// </summary>
    public string Template { get; private set; }

    /// <summary>
    /// 视图配置选项
    /// </summary>
    public Action<IViewEngineOptionsBuilder> TemplateOptionsBuilder { get; private set; }

    /// <summary>
    /// 模型数据
    /// </summary>
    public (Type Type, object Model) TemplateModel { get; private set; } = (typeof(object), default);

    /// <summary>
    /// 模板缓存名称（不含拓展名）
    /// </summary>
    public string TemplateCachedFileName { get; private set; }

    /// <summary>
    /// 视图模板服务作用域
    /// </summary>
    public IServiceProvider ViewEngineScoped { get; private set; }
}