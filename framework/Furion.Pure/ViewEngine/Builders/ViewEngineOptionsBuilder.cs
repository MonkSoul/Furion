// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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