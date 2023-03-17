// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
            Reflect.GetAssembly("System.Linq.Expressions"),
            Reflect.GetAssembly("System.Collections")
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