// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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