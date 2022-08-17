// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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

namespace Furion.DependencyInjection;

/// <summary>
/// 命名服务提供器
/// </summary>
/// <typeparam name="TService">目标服务接口</typeparam>
public interface INamedServiceProvider<TService>
    where TService : class
{
    /// <summary>
    /// 根据服务名称获取服务
    /// </summary>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    TService GetService(string serviceName);

    /// <summary>
    /// 根据服务名称获取服务
    /// </summary>
    /// <typeparam name="ILifetime">服务生存周期接口，<see cref="ITransient"/>，<see cref="IScoped"/>，<see cref="ISingleton"/></typeparam>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    TService GetService<ILifetime>(string serviceName)
        where ILifetime : IPrivateDependency;

    /// <summary>
    /// 根据服务名称获取服务
    /// </summary>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    TService GetRequiredService(string serviceName);

    /// <summary>
    /// 根据服务名称获取服务
    /// </summary>
    /// <typeparam name="ILifetime">服务生存周期接口，<see cref="ITransient"/>，<see cref="IScoped"/>，<see cref="ISingleton"/></typeparam>
    /// <param name="serviceName">服务名称</param>
    /// <returns></returns>
    TService GetRequiredService<ILifetime>(string serviceName)
        where ILifetime : IPrivateDependency;
}