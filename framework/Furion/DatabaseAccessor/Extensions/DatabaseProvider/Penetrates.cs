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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 数据库上下文描述器
    /// </summary>
    internal static readonly ConcurrentDictionary<Type, Type> DbContextDescriptors;

    /// <summary>
    /// 构造函数
    /// </summary>
    static Penetrates()
    {
        DbContextDescriptors = new ConcurrentDictionary<Type, Type>();
    }

    /// <summary>
    /// 配置 SqlServer 数据库上下文
    /// </summary>
    /// <param name="optionBuilder">数据库上下文选项构建器</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns></returns>
    internal static Action<IServiceProvider, DbContextOptionsBuilder> ConfigureDbContext(Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
    {
        return (serviceProvider, options) =>
        {
            // 只有开发环境开启
            if (App.HostEnvironment?.IsDevelopment() ?? false)
            {
                options/*.UseLazyLoadingProxies()*/
                         .EnableDetailedErrors()
                         .EnableSensitiveDataLogging();
            }

            optionBuilder?.Invoke(serviceProvider, options);

            // 添加拦截器
            AddInterceptors(interceptors, options);
        };
    }

    /// <summary>
    /// 检查数据库上下文是否绑定
    /// </summary>
    /// <param name="dbContextLocatorType"></param>
    /// <param name="dbContextType"></param>
    /// <returns></returns>
    internal static void CheckDbContextLocator(Type dbContextLocatorType, out Type dbContextType)
    {
        if (!DbContextDescriptors.TryGetValue(dbContextLocatorType, out dbContextType)) throw new InvalidCastException($" The dbcontext locator `{dbContextLocatorType.Name}` is not bind.");
    }

    /// <summary>
    /// 数据库数据库拦截器
    /// </summary>
    /// <param name="interceptors">拦截器</param>
    /// <param name="options"></param>
    private static void AddInterceptors(IInterceptor[] interceptors, DbContextOptionsBuilder options)
    {
        // 添加拦截器
        var interceptorList = DbProvider.GetDefaultInterceptors();

        if (interceptors != null || interceptors.Length > 0)
        {
            interceptorList.AddRange(interceptors);
        }
        options.AddInterceptors(interceptorList.ToArray());
    }
}