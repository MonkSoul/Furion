// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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