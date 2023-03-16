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

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Furion.DependencyInjection;

/// <summary>
/// 命名服务提供器默认实现
/// </summary>
/// <typeparam name="TService">目标服务接口</typeparam>
internal class NamedServiceProvider<TService> : INamedServiceProvider<TService>
    where TService : class
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public NamedServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 根据服务名称获取服务
    /// </summary>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    public TService GetService(string serviceName)
    {
        // 解析所有实现
        return _serviceProvider.GetServices<TService>()
            .Where(u => ResovleServiceName(u.GetType()) == serviceName)
            .FirstOrDefault();
    }

    /// <summary>
    /// 根据服务名称获取服务
    /// </summary>
    /// <typeparam name="ILifetime">服务生存周期接口，<see cref="ITransient"/>，<see cref="IScoped"/>，<see cref="ISingleton"/></typeparam>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    public TService GetService<ILifetime>(string serviceName)
         where ILifetime : IPrivateDependency
    {
        var resolveNamed = _serviceProvider.GetService<Func<string, ILifetime, object>>();
        return resolveNamed == null ? default : resolveNamed(serviceName, default) as TService;
    }

    /// <summary>
    /// 根据服务名称获取服务
    /// </summary>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    public TService GetRequiredService(string serviceName)
    {
        // 解析所有实现
        var service = _serviceProvider.GetServices<TService>()
            .Where(u => ResovleServiceName(u.GetType()) == serviceName)
            .FirstOrDefault();

        // 如果服务不存在，抛出异常
        return service ?? throw new InvalidOperationException($"Named service `{serviceName}` is not registered in container.");
    }

    /// <summary>
    /// 根据服务名称获取服务
    /// </summary>
    /// <typeparam name="ILifetime">服务生存周期接口，<see cref="ITransient"/>，<see cref="IScoped"/>，<see cref="ISingleton"/></typeparam>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    public TService GetRequiredService<ILifetime>(string serviceName)
         where ILifetime : IPrivateDependency
    {
        var resolveNamed = _serviceProvider.GetRequiredService<Func<string, ILifetime, object>>();
        var service = resolveNamed == null ? default : resolveNamed(serviceName, default) as TService;

        // 如果服务不存在，抛出异常
        return service ?? throw new InvalidOperationException($"Named service `{serviceName}` is not registered in container.");
    }

    /// <summary>
    /// 解析服务名称
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static string ResovleServiceName(Type type)
    {
        if (type.IsDefined(typeof(InjectionAttribute)))
        {
            return type.GetCustomAttribute<InjectionAttribute>().Named;
        }

        return type.Name;
    }
}