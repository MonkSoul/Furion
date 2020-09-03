// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Fur;
using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;

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

            // 注册非泛型仓储
            services.TryAddScoped<IRepository, EFCoreRepository>();

            // 注册泛型仓储
            services.TryAddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));

            // 注册多上下文仓储
            services.TryAddScoped(typeof(IRepository<,>), typeof(EFCoreRepository<,>));

            // 解析数据库上下文
            services.AddScoped(provider =>
            {
                DbContext dbContextResolve(Type locator)
                {
                    // 判断定位器是否绑定了数据库上下文
                    var isRegistered = dbContextLocators.TryGetValue(locator, out var dbContextType);
                    if (!isRegistered) throw new InvalidOperationException("The DbContext for locator binding was not found");

                    return (DbContext)provider.GetService(dbContextType);
                }
                return (Func<Type, DbContext>)dbContextResolve;
            });

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
        public static IServiceCollection AddAppDbContext<TDbContext>(this IServiceCollection services, string connectionString, int poolSize = 100)
            where TDbContext : DbContext
        {
            return services.AddAppDbContext<TDbContext, DbContextLocator>(connectionString, poolSize);
        }

        /// <summary>
        /// 添加其他数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="poolSize">池大小</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddAppDbContext<TDbContext, TDbContextLocator>(this IServiceCollection services, string connectionString, int poolSize = 100)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator, new()
        {
            // 将数据库上下文和定位器保存到上下文栈中
            var isSuccess = dbContextLocators.TryAdd(typeof(TDbContextLocator), typeof(TDbContext));
            if (!isSuccess) throw new InvalidOperationException("The locator is bound to another DbContext");

            // 注册数据库上下文
            services.AddScoped<DbContext, TDbContext>();

            // 添加数据库上下文池
            services.AddDbContextPool<TDbContext>((serviceProvider, options) =>
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

                //options.UseInternalServiceProvider(serviceProvider);
            }
           , poolSize: poolSize);

            return services;
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