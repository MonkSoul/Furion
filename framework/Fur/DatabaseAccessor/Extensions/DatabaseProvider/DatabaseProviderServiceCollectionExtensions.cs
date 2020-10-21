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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Sqlite 数据库服务拓展
    /// </summary>
    [SkipScan]
    public static class DatabaseProviderServiceCollectionExtensions
    {
        /// <summary>
        /// 添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="dynamicDbContext">动态数据库上下文，用于分表分库用</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDbPool<TDbContext>(this IServiceCollection services, string providerName, string connectionString = default, int poolSize = 100, bool dynamicDbContext = false, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext");

            // 注册数据库上下文
            return services.AddDbPool<TDbContext, MasterDbContextLocator>(providerName, connectionString, poolSize, dynamicDbContext, interceptors);
        }

        /// <summary>
        /// 添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="optionBuilder">自定义配置</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDbPool<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionBuilder, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext");

            // 注册数据库上下文
            return services.AddDbPool<TDbContext, MasterDbContextLocator>(optionBuilder, poolSize, interceptors);
        }

        /// <summary>
        /// 添加其他数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="dynamicDbContext">动态数据库上下文，用于分表分库用</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDbPool<TDbContext, TDbContextLocator>(this IServiceCollection services, string providerName, string connectionString = default, int poolSize = 100, bool dynamicDbContext = false, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            var connStr = DbProvider.GetDbContextConnectionString<TDbContext>(connectionString);
            services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext(options => ConfigureDatabase(providerName, connStr, options, dynamicDbContext), interceptors), poolSize: poolSize);

            return services;
        }

        /// <summary>
        /// 添加其他数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="optionBuilder">自定义配置</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDbPool<TDbContext, TDbContextLocator>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionBuilder, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext(optionBuilder, interceptors), poolSize: poolSize);

            return services;
        }

        /// <summary>
        ///  添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="interceptors">拦截器</param>
        /// <param name="dynamicDbContext">动态数据库上下文，用于分表分库用</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDb<TDbContext>(this IServiceCollection services, string providerName, string connectionString = default, bool dynamicDbContext = false, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext");

            // 注册数据库上下文
            return services.AddDb<TDbContext, MasterDbContextLocator>(providerName, connectionString, dynamicDbContext, interceptors);
        }

        /// <summary>
        ///  添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="optionBuilder">自定义配置</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDb<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext");

            // 注册数据库上下文
            return services.AddDb<TDbContext, MasterDbContextLocator>(optionBuilder, interceptors);
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="dynamicDbContext">动态数据库上下文，用于分表分库用</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDb<TDbContext, TDbContextLocator>(this IServiceCollection services, string providerName, string connectionString = default, bool dynamicDbContext = false, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            var connStr = DbProvider.GetDbContextConnectionString<TDbContext>(connectionString);
            services.AddDbContext<TDbContext>(Penetrates.ConfigureDbContext(options => ConfigureDatabase(providerName, connStr, options, dynamicDbContext), interceptors));

            return services;
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="optionBuilder">自定义配置</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDb<TDbContext, TDbContextLocator>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            services.AddDbContext<TDbContext>(Penetrates.ConfigureDbContext(optionBuilder, interceptors));

            return services;
        }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="options">数据库上下文选项构建器</param>
        /// <param name="dynamicDbContext">动态数据库上下文，用于分表分库用</param>
        private static void ConfigureDatabase(string providerName, string connectionString, DbContextOptionsBuilder options, bool dynamicDbContext = false)
        {
            var dbContextOptionsBuilder = options;
            if (!string.IsNullOrEmpty(connectionString))
            {
                // 调用对应数据库程序集
                dbContextOptionsBuilder = GetDatabaseProviderUseMethod(providerName)
                    .Invoke(null, new object[] { options, connectionString, MigrationsAssemblyAction }) as DbContextOptionsBuilder;
            }

            // 解决分表分库
            if (dynamicDbContext) dbContextOptionsBuilder
                 .ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();
        }

        /// <summary>
        /// 数据库提供器 UseXXX 方法缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, MethodInfo> DatabaseProviderUseMethodCollection;

        /// <summary>
        /// 配置Code First 程序集 Action委托
        /// </summary>
        private static readonly Action<IRelationalDbContextOptionsBuilderInfrastructure> MigrationsAssemblyAction;

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static DatabaseProviderServiceCollectionExtensions()
        {
            DatabaseProviderUseMethodCollection = new ConcurrentDictionary<string, MethodInfo>();
            MigrationsAssemblyAction = options => options.GetType()
                .GetMethod("MigrationsAssembly")
                .Invoke(options, new[] { Penetrates.MigrationAssemblyName });
        }

        /// <summary>
        /// 获取数据库提供器对应的 useXXX 方法
        /// </summary>
        /// <param name="providerName">数据库提供器</param>
        /// <returns></returns>
        private static MethodInfo GetDatabaseProviderUseMethod(string providerName)
        {
            return DatabaseProviderUseMethodCollection.GetOrAdd(providerName, Function);

            // 本地静态方法
            static MethodInfo Function(string providerName)
            {
                // 加载对应的数据库提供器程序集
                var databaseProviderAssembly = Assembly.Load(providerName);

                // 数据库提供器服务拓展类型名
                var databaseProviderServiceExtensionTypeName = providerName switch
                {
                    DbProvider.SqlServer => "SqlServerDbContextOptionsExtensions",
                    DbProvider.Sqlite => "SqliteDbContextOptionsBuilderExtensions",
                    DbProvider.Cosmos => "CosmosDbContextOptionsExtensions",
                    DbProvider.InMemoryDatabase => "InMemoryDbContextOptionsExtensions",
                    DbProvider.MySql => "MySqlDbContextOptionsExtensions",
                    DbProvider.Npgsql => "NpgsqlDbContextOptionsBuilderExtensions",
                    DbProvider.Oracle => "OracleDbContextOptionsExtensions",
                    DbProvider.Firebird => "FbDbContextOptionsBuilderExtensions",
                    DbProvider.Dm => "DmDbContextOptionsExtensions",
                    _ => null
                };

                // 加载拓展类型
                var databaseProviderServiceExtensionType = databaseProviderAssembly.GetType($"Microsoft.EntityFrameworkCore.{databaseProviderServiceExtensionTypeName}");

                // useXXX方法名
                var useMethodName = providerName switch
                {
                    DbProvider.SqlServer => $"Use{nameof(DbProvider.SqlServer)}",
                    DbProvider.Sqlite => $"Use{nameof(DbProvider.Sqlite)}",
                    DbProvider.Cosmos => $"Use{nameof(DbProvider.Cosmos)}",
                    DbProvider.InMemoryDatabase => $"Use{nameof(DbProvider.InMemoryDatabase)}",
                    DbProvider.MySql => $"Use{nameof(DbProvider.MySql)}",
                    DbProvider.Npgsql => $"Use{nameof(DbProvider.Npgsql)}",
                    DbProvider.Oracle => $"Use{nameof(DbProvider.Oracle)}",
                    DbProvider.Firebird => $"Use{nameof(DbProvider.Firebird)}",
                    DbProvider.Dm => $"Use{nameof(DbProvider.Dm)}",
                    _ => null
                };

                // 获取UseXXX方法
                var useMethod = databaseProviderServiceExtensionType
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(u => u.Name == useMethodName && !u.IsGenericMethod && u.GetParameters().Length == 3 && u.GetParameters()[1].ParameterType == typeof(string));

                return useMethod;
            }
        }
    }
}