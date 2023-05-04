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