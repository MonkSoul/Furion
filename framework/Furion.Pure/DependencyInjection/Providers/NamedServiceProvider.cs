// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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