using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 常量、公共方法配置类
    /// </summary>
    [SkipScan]
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
                if (App.WebHostEnvironment.IsDevelopment())
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
            if (App.Settings.InjectMiniProfiler != true) return;

            // 添加拦截器
            var interceptorList = new List<IInterceptor>
            {
                new SqlConnectionProfilerInterceptor(),
                new SqlCommandProfilerInterceptor(),
                new DbContextSaveChangesInterceptor()
            };
            if (interceptors != null || interceptors.Length > 0)
            {
                interceptorList.AddRange(interceptors);
            }
            options.AddInterceptors(interceptorList.ToArray());
        }
    }
}