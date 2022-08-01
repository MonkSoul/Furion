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
/// 视图编译构建器
/// </summary>
[SuppressSniffer]
public class ViewEngineOptionsBuilder : IViewEngineOptionsBuilder
{
    /// <summary>
    /// 视图编译选项
    /// </summary>
    public ViewEngineOptions Options { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    public ViewEngineOptionsBuilder(ViewEngineOptions options = null)
    {
        Options = options ?? new ViewEngineOptions();
    }

    /// <summary>
    /// 添加程序集引用
    /// </summary>
    /// <param name="assemblyName"></param>
    public void AddAssemblyReferenceByName(string assemblyName)
    {
        var assembly = Reflect.GetAssembly(assemblyName);
        AddAssemblyReference(assembly);
    }

    /// <summary>
    /// 添加程序集引用
    /// </summary>
    /// <param name="assembly"></param>
    public void AddAssemblyReference(Assembly assembly)
    {
        Options.ReferencedAssemblies.Add(assembly);
    }

    /// <summary>
    /// 添加程序集引用
    /// </summary>
    /// <param name="type"></param>
    public void AddAssemblyReference(Type type)
    {
        AddAssemblyReference(type.Assembly);

        foreach (var argumentType in type.GenericTypeArguments)
        {
            AddAssemblyReference(argumentType);
        }
    }

    /// <summary>
    /// 添加元数据引用
    /// </summary>
    /// <param name="reference"></param>
    public void AddMetadataReference(MetadataReference reference)
    {
        Options.MetadataReferences.Add(reference);
    }

    /// <summary>
    /// 添加 Using
    /// </summary>
    /// <param name="namespaceName"></param>
    public void AddUsing(string namespaceName)
    {
        Options.DefaultUsings.Add(namespaceName);
    }

    /// <summary>
    /// 添加继承类型
    /// </summary>
    /// <param name="type"></param>
    public void Inherits(Type type)
    {
        Options.Inherits = RenderTypeName(type);
        AddAssemblyReference(type);
    }

    /// <summary>
    /// 渲染类型名
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string RenderTypeName(Type type)
    {
        var result = type.Namespace + "." + type.Name;

        if (result.Contains('`'))
        {
            result = result[..result.IndexOf("`")];
        }

        if (type.GenericTypeArguments.Length == 0)
        {
            return result;
        }

        return result + "<" + string.Join(",", type.GenericTypeArguments.Select(RenderTypeName)) + ">";
    }
}