// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 常量、公共方法配置类
    /// </summary>
    internal static class Penetrates
    {
        /// <summary>
        /// 数据库上下文和定位器缓存
        /// </summary>
        internal static readonly ConcurrentDictionary<Type, Type> DbContextWithLocatorCached;

        /// <summary>
        /// 数据库上下文定位器缓存
        /// </summary>
        internal static readonly ConcurrentDictionary<string, Type> DbContextLocatorTypeCached;

        /// <summary>
        /// 构造函数
        /// </summary>
        static Penetrates()
        {
            DbContextWithLocatorCached = new ConcurrentDictionary<Type, Type>();
            DbContextLocatorTypeCached = new ConcurrentDictionary<string, Type>();
        }

        /// <summary>
        /// 配置 SqlServer 数据库上下文
        /// </summary>
        /// <param name="optionBuilder">数据库上下文选项构建器</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns></returns>
        internal static Action<IServiceProvider, DbContextOptionsBuilder> ConfigureDbContext(Action<DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
        {
            return (serviceProvider, options) =>
            {
                // 只有开发环境开启
                if (App.HostEnvironment.IsDevelopment())
                {
                    options/*.UseLazyLoadingProxies()*/
                             .EnableDetailedErrors()
                             .EnableSensitiveDataLogging();
                }

                optionBuilder.Invoke(options);

                // 添加拦截器
                AddInterceptors(interceptors, options);

                // .NET 5 版本已不再起作用
                // options.UseInternalServiceProvider(serviceProvider);
            };
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
}