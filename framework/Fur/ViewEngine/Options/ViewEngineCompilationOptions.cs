using Fur.DependencyInjection;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

// reference：https://github.com/adoconnection/RazorEngineCore
namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图编译选项
    /// </summary>
    [SkipScan]
    public class ViewEngineCompilationOptions
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewEngineCompilationOptions()
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var isFullFramework = RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework", StringComparison.OrdinalIgnoreCase);

            if (isWindows && isFullFramework)
            {
                ReferencedAssemblies = new HashSet<Assembly>()
                {
                    typeof(object).Assembly,
                    Assembly.Load(new AssemblyName("Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")),
                    typeof(ViewEngineTemplate).Assembly,
                    typeof(System.Runtime.GCSettings).Assembly,
                    typeof(System.Linq.Enumerable).Assembly,
                    typeof(System.Linq.Expressions.Expression).Assembly,
                    Assembly.Load(new AssemblyName("netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"))
                };
            }

            if (isWindows && !isFullFramework)
            {
                ReferencedAssemblies = new HashSet<Assembly>()
                {
                    typeof(object).Assembly,
                    Assembly.Load(new AssemblyName("Microsoft.CSharp")),
                    typeof(ViewEngineTemplate).Assembly,
                    Assembly.Load(new AssemblyName("System.Runtime")),
                    Assembly.Load(new AssemblyName("System.Linq")),
                    Assembly.Load(new AssemblyName("System.Linq.Expressions")),
                    Assembly.Load(new AssemblyName("netstandard"))
                };
            }

            if (!isWindows)
            {
                ReferencedAssemblies = new HashSet<Assembly>()
                {
                    typeof(object).Assembly,
                    Assembly.Load(new AssemblyName("Microsoft.CSharp")),
                    typeof(ViewEngineTemplate).Assembly,
                    Assembly.Load(new AssemblyName("System.Runtime")),
                    Assembly.Load(new AssemblyName("System.Linq")),
                    Assembly.Load(new AssemblyName("System.Linq.Expressions")),
                    Assembly.Load(new AssemblyName("netstandard"))
                };
            }
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
        public string TemplateNamespace { get; set; } = "TemplateNamespace";

        /// <summary>
        /// 继承
        /// </summary>
        public string Inherits { get; set; } = "Fur.ViewEngine.ViewEngineTemplate";

        /// <summary>
        /// 默认 Using
        /// </summary>
        public HashSet<string> DefaultUsings { get; set; } = new HashSet<string>()
        {
            "System.Linq"
        };
    }
}