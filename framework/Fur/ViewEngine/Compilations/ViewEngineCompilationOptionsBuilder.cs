// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using System.Reflection;

namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图编译选项构建器实现类
    /// </summary>
    [NonBeScan]
    public class ViewEngineCompilationOptionsBuilder : IViewEngineCompilationOptionsBuilder
    {
        public ViewEngineCompilationOptions Options { get; set; }

        public ViewEngineCompilationOptionsBuilder(ViewEngineCompilationOptions options = null)
        {
            Options = options ?? new ViewEngineCompilationOptions();
        }

        public void AddAssemblyReferenceByName(string assemblyName)
        {
            var assembly = Assembly.Load(new AssemblyName(assemblyName));
            AddAssemblyReference(assembly);
        }

        public void AddAssemblyReference(Assembly assembly)
        {
            Options.ReferencedAssemblies.Add(assembly);
        }

        public void AddAssemblyReference(Type type)
        {
            AddAssemblyReference(type.Assembly);

            foreach (var argumentType in type.GenericTypeArguments)
            {
                AddAssemblyReference(argumentType);
            }
        }

        public void AddMetadataReference(MetadataReference reference)
        {
            Options.MetadataReferences.Add(reference);
        }

        public void AddUsing(string namespaceName)
        {
            Options.DefaultUsings.Add(namespaceName);
        }

        public void Inherits(Type type)
        {
            Options.Inherits = RenderTypeName(type);
            AddAssemblyReference(type);
        }

        private string RenderTypeName(Type type)
        {
            var result = type.Namespace + "." + type.Name;

            if (result.Contains('`'))
            {
                result = result.Substring(0, result.IndexOf("`"));
            }

            if (type.GenericTypeArguments.Length == 0)
            {
                return result;
            }

            return result + "<" + string.Join(",", type.GenericTypeArguments.Select(RenderTypeName)) + ">";
        }
    }
}