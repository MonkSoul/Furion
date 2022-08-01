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

using Furion.Reflection;
using Microsoft.CodeAnalysis;
using System.Reflection;

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎编译选项
/// </summary>
[SuppressSniffer]
public class ViewEngineOptions
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ViewEngineOptions()
    {
        ReferencedAssemblies = new HashSet<Assembly>()
            {
                typeof(object).Assembly,
                typeof(ViewEngineModel).Assembly,
                typeof(System.Collections.IList).Assembly,
                typeof(IEnumerable<>).Assembly,
                Reflect.GetAssembly("Microsoft.CSharp"),
                Reflect.GetAssembly("System.Runtime"),
                Reflect.GetAssembly("System.Linq"),
                Reflect.GetAssembly("System.Linq.Expressions")
            };
    }

    /// <summary>
    /// 引用程序集
    /// </summary>
    public HashSet<Assembly> ReferencedAssemblies { get; set; }

    /// <summary>
    /// 元数据引用
    /// </summary>
    public HashSet<MetadataReference> MetadataReferences { get; set; } = new HashSet<MetadataReference>();

    /// <summary>
    /// 模板命名空间
    /// </summary>
    public string TemplateNamespace { get; set; } = "Furion.ViewEngine";

    /// <summary>
    /// 继承
    /// </summary>
    public string Inherits { get; set; } = "Furion.ViewEngine.Template.Models";

    /// <summary>
    /// 默认 Using
    /// </summary>
    public HashSet<string> DefaultUsings { get; set; } = new HashSet<string>()
        {
            "System.Linq",
            "System.Collections",
            "System.Collections.Generic"
        };
}