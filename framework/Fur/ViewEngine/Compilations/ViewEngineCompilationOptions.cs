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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图编译选项
    /// </summary>
    [NonBeScan]
    public class ViewEngineCompilationOptions
    {
        public HashSet<Assembly> ReferencedAssemblies { get; set; } = DefaultReferencedAssemblies();

        public HashSet<MetadataReference> MetadataReferences { get; set; } = new HashSet<MetadataReference>();
        public string TemplateNamespace { get; set; } = "TemplateNamespace";
        public string Inherits { get; set; } = "ViewEngineCore.ViewEngineTemplateBase";

        public HashSet<string> DefaultUsings { get; set; } = new HashSet<string>()
        {
            "System.Linq"
        };

        public ViewEngineCompilationOptions()
        {
            // Loading netstandard explicitly causes runtime error on Linux/OSX
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework", StringComparison.OrdinalIgnoreCase))
                {
                    ReferencedAssemblies.Add(
                        Assembly.Load(
                            new AssemblyName(
                                "netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51")));
                }
                else
                {
                    ReferencedAssemblies.Add(Assembly.Load(new AssemblyName("netstandard")));
                }
            }
        }

        private static HashSet<Assembly> DefaultReferencedAssemblies()
        {
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework", StringComparison.OrdinalIgnoreCase))
            {
                return new HashSet<Assembly>()
                           {
                               typeof(object).Assembly,
                               Assembly.Load(new AssemblyName("Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")),
                               typeof(ViewEngineTemplate).Assembly,
                               typeof(System.Runtime.GCSettings).Assembly,
                               typeof(System.Linq.Enumerable).Assembly,
                               typeof(System.Linq.Expressions.Expression).Assembly
                           };
            }

            return new HashSet<Assembly>()
                       {
                           typeof(object).Assembly,
                           Assembly.Load(new AssemblyName("Microsoft.CSharp")),
                           typeof(ViewEngineTemplate).Assembly,
                           Assembly.Load(new AssemblyName("System.Runtime")),
                           Assembly.Load(new AssemblyName("System.Linq")),
                           Assembly.Load(new AssemblyName("System.Linq.Expressions"))
                       };
        }
    }
}