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

using Microsoft.CodeAnalysis;
using System.Reflection;

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎选项构建器
/// </summary>
public interface IViewEngineOptionsBuilder
{
    /// <summary>
    /// 视图编译选项
    /// </summary>
    ViewEngineOptions Options { get; set; }

    /// <summary>
    /// 添加程序集引用
    /// </summary>
    /// <param name="assemblyName"></param>
    void AddAssemblyReferenceByName(string assemblyName);

    /// <summary>
    /// 添加程序集引用
    /// </summary>
    /// <param name="assembly"></param>
    void AddAssemblyReference(Assembly assembly);

    /// <summary>
    /// 添加程序集引用
    /// </summary>
    /// <param name="type"></param>
    void AddAssemblyReference(Type type);

    /// <summary>
    /// 添加元数据引用
    /// </summary>
    /// <param name="reference"></param>
    void AddMetadataReference(MetadataReference reference);

    /// <summary>
    /// 添加 Using
    /// </summary>
    /// <param name="namespaceName"></param>
    void AddUsing(string namespaceName);

    /// <summary>
    /// 添加继承类型
    /// </summary>
    /// <param name="type"></param>
    void Inherits(Type type);
}