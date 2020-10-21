// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 数据库访问器服务拓展类
    /// </summary>
    [SkipScan]
    public static class DatabaseAccessorServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">配置</param>
        /// <param name="migrationAssemblyName">迁移类库名称</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDatabaseAccessor(this IServiceCollection services, Action<IServiceCollection> configure = null, string migrationAssemblyName = default)
        {
            // 设置迁移类库名称
            if (!string.IsNullOrEmpty(migrationAssemblyName)) Penetrates.MigrationAssemblyName = migrationAssemblyName;

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

            // 注册主从库仓储
            services.TryAddScoped(typeof(IMSRepository<,>), typeof(MSRepository<,>));
            services.TryAddScoped(typeof(IMSRepository<,,>), typeof(MSRepository<,,>));
            services.TryAddScoped(typeof(IMSRepository<,,,>), typeof(MSRepository<,,,>));
            services.TryAddScoped(typeof(IMSRepository<,,,,>), typeof(MSRepository<,,,,>));
            services.TryAddScoped(typeof(IMSRepository<,,,,,>), typeof(MSRepository<,,,,,>));
            services.TryAddScoped(typeof(IMSRepository<,,,,,,>), typeof(MSRepository<,,,,,,>));
            services.TryAddScoped(typeof(IMSRepository<,,,,,,,>), typeof(MSRepository<,,,,,,,>));

            // 注册非泛型仓储
            services.TryAddScoped<IRepository, EFCoreRepository>();

            // 解析数据库上下文
            services.AddTransient(provider =>
            {
                DbContext dbContextResolve(Type locator, ITransient transient)
                {
                    // 判断定位器是否绑定了数据库上下文
                    var isRegistered = Penetrates.DbContextWithLocatorCached.TryGetValue(locator, out var dbContextType);
                    if (!isRegistered) throw new InvalidOperationException("The DbContext for locator binding was not found");

                    // 动态解析数据库上下文
                    return provider.GetService(dbContextType) as DbContext;
                }
                return (Func<Type, ITransient, DbContext>)dbContextResolve;
            });

            services.AddScoped(provider =>
            {
                DbContext dbContextResolve(Type locator, IScoped scoped)
                {
                    // 判断定位器是否绑定了数据库上下文
                    var isRegistered = Penetrates.DbContextWithLocatorCached.TryGetValue(locator, out var dbContextType);
                    if (!isRegistered) throw new InvalidOperationException("The DbContext for locator binding was not found");

                    // 动态解析数据库上下文
                    return provider.GetService(dbContextType) as DbContext;
                }
                return (Func<Type, IScoped, DbContext>)dbContextResolve;
            });

            // 注册 Sql 代理接口
            services.AddScopedDispatchProxyForInterface<SqlDispatchProxy, ISqlDispatchProxy>();

            // 注册全局工作单元过滤器
            services.Configure<MvcOptions>(options => options.Filters.Add<UnitOfWorkFilter>());

            return services;
        }

        /// <summary>
        /// 注册数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务提供器</param>
        public static IServiceCollection RegisterDbContext<TDbContext, TDbContextLocator>(this IServiceCollection services)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            var dbContextLocatorType = (typeof(TDbContextLocator));

            // 将数据库上下文和定位器一一保存起来
            var isSuccess = Penetrates.DbContextWithLocatorCached.TryAdd(dbContextLocatorType, typeof(TDbContext));
            Penetrates.DbContextLocatorTypeCached.TryAdd(dbContextLocatorType.FullName, dbContextLocatorType);

            if (!isSuccess) throw new InvalidOperationException("The locator is bound to another DbContext");

            // 注册数据库上下文
            services.TryAddScoped<TDbContext>();

            return services;
        }
    }
}