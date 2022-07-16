// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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