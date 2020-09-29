// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur 
// 				    Github：https://github.com/monksoul/Fur 
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Sqlite 数据库服务拓展
    /// </summary>
    [SkipScan]
    public static class SqliteServiceCollectionExtensions
    {
        /// <summary>
        /// 添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSqlitePool<TDbContext>(this IServiceCollection services, string connectionString = default, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(DbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext");

            // 注册数据库上下文
            return services.AddSqlitePool<TDbContext, DbContextLocator>(connectionString, poolSize, interceptors);
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
        public static IServiceCollection AddSqlitePool<TDbContext, TDbContextLocator>(this IServiceCollection services, string connectionString = default, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            var connStr = Penetrates.GetDbContextConnectionString<TDbContext>(connectionString);
            services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext(connStr, options => ConfigureSqlite(connStr, options), interceptors), poolSize: poolSize);

            return services;
        }

        /// <summary>
        ///  添加默认 Sqlite 数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSqlite<TDbContext>(this IServiceCollection services, string connectionString = default, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(DbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext");

            // 注册数据库上下文
            return services.AddSqlite<TDbContext, DbContextLocator>(connectionString, interceptors);
        }

        /// <summary>
        /// 添加 Sqlite 数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddSqlite<TDbContext, TDbContextLocator>(this IServiceCollection services, string connectionString = default, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            var connStr = Penetrates.GetDbContextConnectionString<TDbContext>(connectionString);
            services.AddDbContext<TDbContext>(Penetrates.ConfigureDbContext(connStr, options => ConfigureSqlite(connStr, options), interceptors));

            return services;
        }

        /// <summary>
        /// 配置Sqlite
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="options">数据库上下文选项构建器</param>
        private static void ConfigureSqlite(string connectionString, DbContextOptionsBuilder options)
        {
            options.UseSqlite(connectionString, options =>
            {
                // 配置全局切割 Sql，而不是生成单个复杂 sql
                //options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                // 配置 code first 程序集
                options.MigrationsAssembly("Fur.Database.Migrations");

                //options.EnableRetryOnFailure();
                //options.MigrationsHistoryTable("__EFMigrationsHistory", "fur");
            });
        }
    }
}