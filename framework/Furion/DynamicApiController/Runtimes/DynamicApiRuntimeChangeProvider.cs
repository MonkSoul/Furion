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

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;

namespace Furion.DynamicApiController;

/// <summary>
/// 动态 WebAPI 运行时感知提供器
/// </summary>
internal class DynamicApiRuntimeChangeProvider : IDynamicApiRuntimeChangeProvider
{
    /// <summary>
    /// 应用程序部件管理器
    /// </summary>
    private readonly ApplicationPartManager _applicationPartManager;

    /// <summary>
    /// MVC 控制器感知提供器
    /// </summary>
    private readonly MvcActionDescriptorChangeProvider _mvcActionDescriptorChangeProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="applicationPartManager">应用程序部件管理器</param>
    /// <param name="mvcActionDescriptorChangeProvider">MVC 控制器感知提供器</param>
    public DynamicApiRuntimeChangeProvider(ApplicationPartManager applicationPartManager
        , MvcActionDescriptorChangeProvider mvcActionDescriptorChangeProvider)
    {
        _applicationPartManager = applicationPartManager;
        _mvcActionDescriptorChangeProvider = mvcActionDescriptorChangeProvider;
    }

    /// <summary>
    /// 添加程序集
    /// </summary>
    /// <param name="assemblies">程序集</param>
    public void AddAssemblies(params Assembly[] assemblies)
    {
        if (assemblies != null && assemblies.Length > 0)
        {
            foreach (var assembly in assemblies)
            {
                _applicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly));
            }
        }
    }

    /// <summary>
    /// 添加程序集并立即感知变化
    /// </summary>
    /// <param name="assemblies">程序集</param>
    public void AddAssembliesWithNotifyChanges(params Assembly[] assemblies)
    {
        if (assemblies != null && assemblies.Length > 0)
        {
            AddAssemblies(assemblies);
            NotifyChanges();
        }
    }

    /// <summary>
    /// 移除程序集
    /// </summary>
    /// <param name="assemblyNames">程序集名称</param>
    public void RemoveAssemblies(params string[] assemblyNames)
    {
        if (assemblyNames != null && assemblyNames.Length > 0)
        {
            foreach (var assemblyName in assemblyNames)
            {
                var applicationPart = _applicationPartManager.ApplicationParts.FirstOrDefault(p => p.Name == assemblyName);
                if (applicationPart != null) _applicationPartManager.ApplicationParts.Remove(applicationPart);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    /// <summary>
    /// 移除程序集
    /// </summary>
    /// <param name="assemblies">程序集</param>
    public void RemoveAssemblies(params Assembly[] assemblies)
    {
        if (assemblies != null && assemblies.Length > 0)
        {
            RemoveAssemblies(assemblies.Select(ass => ass.GetName().Name).ToArray());
        }
    }

    /// <summary>
    /// 移除程序集并立即感知变化
    /// </summary>
    /// <param name="assemblyNames">程序集名称</param>
    public void RemoveAssembliesWithNotifyChanges(params string[] assemblyNames)
    {
        if (assemblyNames != null && assemblyNames.Length > 0)
        {
            RemoveAssemblies(assemblyNames);
            NotifyChanges();
        }
    }

    /// <summary>
    /// 移除程序集并立即感知变化
    /// </summary>
    /// <param name="assemblies">程序集</param>
    public void RemoveAssembliesWithNotifyChanges(params Assembly[] assemblies)
    {
        if (assemblies != null && assemblies.Length > 0)
        {
            RemoveAssemblies(assemblies);
            NotifyChanges();
        }
    }

    /// <summary>
    /// 感知变化
    /// </summary>
    public void NotifyChanges()
    {
        _mvcActionDescriptorChangeProvider.NotifyChanges();
    }
}