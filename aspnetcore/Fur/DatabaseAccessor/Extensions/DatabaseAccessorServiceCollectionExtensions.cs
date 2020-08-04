using Fur.DatabaseAccessor.Interceptors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 数据库操作上下文服务拓展类
    /// </summary>

    public static class DatabaseAccessorServiceCollectionExtensions
    {
        /// <summary>
        /// 配置数据库上下文池
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="env">web环境</param>
        /// <param name="poolSize">数据库上下文池最大存放数量</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddFurSqlServerDbContextPool<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            //services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<TDbContext>((serviceProvider, options) =>
            {
                if (env.IsDevelopment())
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

                if (interceptors == null || !interceptors.Any())
                {
                    interceptors = new IInterceptor[] { new SqlConnectionProfilerInterceptor() };
                }
                options.AddInterceptors(interceptors);

                //options.UseInternalServiceProvider(serviceProvider);
            }
            , poolSize: poolSize);

            return services;
        }

        /// <summary>
        /// 配置数据库上下文
        /// <para>推荐使用 <see cref="AddFurSqlServerDbContextPool{TDbContext}(IServiceCollection, string, IWebHostEnvironment, int)"/></para>
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="env">web环境</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddFurSqlServerDbContext<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            //services.AddEntityFrameworkSqlServer();
            services.AddDbContext<TDbContext>((serviceProvider, options) =>
            {
                if (env.IsDevelopment())
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

                if (interceptors == null || !interceptors.Any())
                {
                    interceptors = new IInterceptor[] { new SqlConnectionProfilerInterceptor() };
                }
                options.AddInterceptors(interceptors);

                //options.UseInternalServiceProvider(serviceProvider);
            });

            return services;
        }
    }
}