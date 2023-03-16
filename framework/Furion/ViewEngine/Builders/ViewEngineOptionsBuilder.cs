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
        IList<string> elements = new List<string>()
        {
            type.Namespace,
            RenderDeclaringType(type.DeclaringType),
            type.Name
        };

        var result = string.Join(".", elements.Where(e => !string.IsNullOrWhiteSpace(e)));

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

    private string RenderDeclaringType(Type type)
    {
        if (type == null)
        {
            return null;
        }

        var parent = RenderDeclaringType(type.DeclaringType);

        if (string.IsNullOrWhiteSpace(parent))
        {
            return type.Name;
        }

        return parent + "." + type.Name;
    }
}