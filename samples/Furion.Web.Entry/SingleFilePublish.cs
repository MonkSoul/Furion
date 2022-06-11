using System.Reflection;

namespace Furion.Web.Entry;

/// <summary>
/// 解决单文件发布问题
/// </summary>
public class SingleFilePublish : ISingleFilePublish
{
    /// <summary>
    /// 解决单文件不能扫描的程序集
    /// </summary>
    /// <remarks>和 <see cref="IncludeAssemblyNames"/> 可同时配置</remarks>
    /// <returns></returns>
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    /// <summary>
    /// 解决单文件不能扫描的程序集名称
    /// </summary>
    /// <remarks>和 <see cref="IncludeAssemblies"/> 可同时配置</remarks>
    /// <returns></returns>
    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "Furion.Application",
            "Furion.Core",
            "Furion.EntityFramework.Core",
            "Furion.Web.Core"
        };
    }
}
