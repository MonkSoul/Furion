using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fur.Mvc.Extensions.Services
{
    /// <summary>
    /// 数据库操作上下文服务拓展类
    /// </summary>
    public static class DatabaseVisitorServiceCollectionExtensions
    {
        #region 配置数据库上下文池信息 + public static IServiceCollection AddFurSqlServerDbContextPool<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env, int poolSize = 100) where TDbContext : DbContext
        /// <summary>
        /// 配置数据库上下文池信息
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="env">web环境</param>
        /// <param name="poolSize">数据库上下文池最大存放数量</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddFurSqlServerDbContextPool<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env, int poolSize = 100)
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
                options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
                //options.UseInternalServiceProvider(serviceProvider);
            }
            , poolSize: poolSize);

            return services;
        }
        #endregion

        #region 配置数据库上下文池信息 + public static IServiceCollection AddFurSqlServerDbContext<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env) where TDbContext : DbContext
        /// <summary>
        /// 配置数据库上下文信息
        /// <para>推荐使用 <see cref="AddFurSqlServerDbContextPool{TDbContext}(IServiceCollection, string, IWebHostEnvironment, int)"/></para>
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="env">web环境</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddFurSqlServerDbContext<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env)
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
                options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
                //options.UseInternalServiceProvider(serviceProvider);
            });

            return services;
        }
        #endregion
    }
}
