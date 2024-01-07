// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Reflection;

namespace Furion;

/// <summary>
/// 解决单文件发布程序集扫描问题
/// </summary>
public interface ISingleFilePublish
{
    /// <summary>
    /// 包含程序集数组
    /// </summary>
    /// <remarks>配置单文件发布扫描程序集</remarks>
    /// <returns></returns>
    Assembly[] IncludeAssemblies();

    /// <summary>
    /// 包含程序集名称数组
    /// </summary>
    /// <remarks>配置单文件发布扫描程序集名称</remarks>
    /// <returns></returns>
    string[] IncludeAssemblyNames();
}