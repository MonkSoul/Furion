// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using System;
using System.Reflection;

// reference：https://github.com/adoconnection/RazorEngineCore
namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图编译构建器
    /// </summary>
    public interface IViewEngineCompilationOptionsBuilder
    {
        /// <summary>
        /// 视图编译选项
        /// </summary>
        ViewEngineCompilationOptions Options { get; set; }

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
}