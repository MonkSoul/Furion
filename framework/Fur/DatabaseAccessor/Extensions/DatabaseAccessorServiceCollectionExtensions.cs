// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur;
using Fur.DatabaseAccessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 数据库访问器服务拓展类
    /// </summary>
    public static class DatabaseAccessorServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDatabaseAccessor(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            // 配置数据库上下文
            configure?.Invoke(services);

            // 注册数据库上下文池
            services.TryAddScoped<IDbContextPool, DbContextPool>();

            // 注册 Sql 仓储
            services.TryAddScoped(typeof(ISqlRepository<>), typeof(SqlRepository<>));

            // 注册 Sql 非泛型仓储
            services.TryAddScoped<ISqlRepository, SqlRepository>();

            // 注册多数据库上下文仓储
            services.TryAddScoped(typeof(IRepository<,>), typeof(EFCoreRepository<,>));

            // 注册泛型仓储
            services.TryAddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));

            // 注册非泛型仓储
            services.TryAddScoped<IRepository, EFCoreRepository>();

            // 解析数据库上下文
            services.AddScoped(provider =>
            {
                DbContext dbContextResolve(Type locator)
                {
                    // 判断定位器是否绑定了数据库上下文
                    var isRegistered = dbContextLocators.TryGetValue(locator, out var dbContextType);
                    if (!isRegistered) throw new InvalidOperationException("The DbContext for locator binding was not found");

                    // 动态解析数据库上下文
                    return provider.GetService(dbContextType) as DbContext;
                }
                return (Func<Type, DbContext>)dbContextResolve;
            });

            // 注册 Sql 代理接口
            services.AddSqlDispatchProxy();

            // 注册全局工作单元过滤器
            services.Configure<MvcOptions>(options => options.Filters.Add<UnitOfWorkFilter>());

            return services;
        }

        /// <summary>
        /// 添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="poolSize">池大小</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSqlServerPool<TDbContext>(this IServiceCollection services, string connectionString, int poolSize = 100)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (dbContextLocators.ContainsKey(typeof(DbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext");

            // 注册数据库上下文
            return services.AddSqlServerPool<TDbContext, DbContextLocator>(connectionString, poolSize);
        }

        /// <summary>
        /// 添加其他数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSqlServerPool<TDbContext, TDbContextLocator>(this IServiceCollection services, string connectionString, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator, new()
        {
            // 将数据库上下文和定位器一一保存起来
            var isSuccess = dbContextLocators.TryAdd(typeof(TDbContextLocator), typeof(TDbContext));
            if (!isSuccess) throw new InvalidOperationException("The locator is bound to another DbContext");

            // 注册数据库上下文
            services.TryAddScoped<TDbContext>();

            // 配置数据库上下文
            services.AddDbContextPool<TDbContext>(ConfigureSqlServerDbContext(connectionString, interceptors), poolSize: poolSize);

            return services;
        }

        /// <summary>
        ///  添加默认 SqlServer 数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSqlServer<TDbContext>(this IServiceCollection services, string connectionString)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (dbContextLocators.ContainsKey(typeof(DbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext");

            // 注册数据库上下文
            return services.AddSqlServer<TDbContext, DbContextLocator>(connectionString);
        }

        /// <summary>
        /// 添加 SqlServer 数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSqlServer<TDbContext, TDbContextLocator>(this IServiceCollection services, string connectionString, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator, new()
        {
            // 将数据库上下文和定位器一一保存起来
            var isSuccess = dbContextLocators.TryAdd(typeof(TDbContextLocator), typeof(TDbContext));
            if (!isSuccess) throw new InvalidOperationException("The locator is bound to another DbContext");

            // 注册数据库上下文
            services.TryAddScoped<TDbContext>();

            // 配置数据库上下文
            services.AddDbContext<TDbContext>(ConfigureSqlServerDbContext(connectionString, interceptors));

            return services;
        }

        /// <summary>
        /// 添加 Sql 代理服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSqlDispatchProxy(this IServiceCollection services)
        {
            // 注册 Sql 代理类
            services.AddScoped<DispatchProxy, SqlDispatchProxy>();

            // Sql 代理依赖接口
            var typeDependency = typeof(ISqlDispatchProxy);

            // 获取所有的代理接口类型
            var sqlDispatchProxyInterfaceTypes = App.Assemblies.SelectMany(u => u.GetTypes()
                .Where(u => typeDependency.IsAssignableFrom(u) && u.IsPublic && u.IsInterface && u != typeDependency));

            // 获取代理创建方法
            var dispatchCreateMethod = typeof(DispatchProxy).GetMethod(nameof(DispatchProxy.Create));

            // 注册 Sql 代理类型
            foreach (var interfaceType in sqlDispatchProxyInterfaceTypes)
            {
                services.AddScoped(interfaceType, provider =>
                {
                    var proxy = dispatchCreateMethod.MakeGenericMethod(interfaceType, typeof(SqlDispatchProxy)).Invoke(null, null);
                    ((SqlDispatchProxy)proxy).ServiceProvider = provider;

                    return proxy;
                });
            }

            return services;
        }

        /// <summary>
        /// 配置 SqlServer 数据库上下文
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns></returns>
        private static Action<IServiceProvider, DbContextOptionsBuilder> ConfigureSqlServerDbContext(string connectionString, params IInterceptor[] interceptors)
        {
            return (serviceProvider, options) =>
            {
                if (App.HostEnvironment.IsDevelopment())
                {
                    options/*.UseLazyLoadingProxies()*/
                                .EnableDetailedErrors()
                                .EnableSensitiveDataLogging();
                }
                options.UseSqlServer(connectionString, options =>
                {
                    //options.EnableRetryOnFailure();
                    //options.MigrationsHistoryTable("__EFMigrationsHistory", "fur");
                    options.MigrationsAssembly("Fur.Database.Migrations");
                });

                // 添加拦截器
                AddInterceptors(interceptors, options);

                //options.UseInternalServiceProvider(serviceProvider);
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
                new SqlConnectionProfilerInterceptor()
            };
            if (interceptors != null || interceptors.Length > 0)
            {
                interceptorList.AddRange(interceptors);
            }
            options.AddInterceptors(interceptorList.ToArray());
        }

        /// <summary>
        /// 数据库上下文定位器集合
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Type> dbContextLocators;

        /// <summary>
        /// 构造函数
        /// </summary>
        static DatabaseAccessorServiceCollectionExtensions()
        {
            dbContextLocators = new ConcurrentDictionary<Type, Type>();
        }
    }
}