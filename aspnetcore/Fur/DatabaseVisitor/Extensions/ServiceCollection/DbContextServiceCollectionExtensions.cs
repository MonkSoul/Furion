using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fur.DatabaseVisitor.Extensions.ServiceCollection
{
    /// <summary>
    /// 数据库操作上下文服务拓展类
    /// </summary>
    public static class DbContextServiceCollectionExtensions
    {
        #region 配置数据库上下文池信息 + public static IServiceCollection AddFurDbContextPool<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env) where TDbContext : DbContext
        /// <summary>
        /// 配置数据库上下文池信息
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="env">web环境</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddFurDbContextPool<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env)
            where TDbContext : DbContext
        {
            //services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<TDbContext>(options =>
            {
                if (env.IsDevelopment())
                {
                    options/*.UseLazyLoadingProxies()*/
                                .EnableDetailedErrors()
                                .EnableSensitiveDataLogging();
                }
                options.UseSqlServer(connectionString);
            }
            , poolSize: 128);

            return services;
        }
        #endregion

        #region 配置数据库上下文池信息 + public static IServiceCollection AddFurDbContext<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env) where TDbContext : DbContext
        /// <summary>
        /// 配置数据库上下文信息
        /// <para>推荐使用 <see cref="AddFurDbContextPool{TDbContext}(IServiceCollection, string, IWebHostEnvironment)"/></para>
        /// </summary>
        /// <typeparam name="TDbContext">数据库操作上下文</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="env">web环境</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddFurDbContext<TDbContext>(this IServiceCollection services, string connectionString, IWebHostEnvironment env)
            where TDbContext : DbContext
        {
            //services.AddEntityFrameworkSqlServer();
            services.AddDbContext<TDbContext>(options =>
            {
                if (env.IsDevelopment())
                {
                    options/*.UseLazyLoadingProxies()*/
                                .EnableDetailedErrors()
                                .EnableSensitiveDataLogging();
                }
                options.UseSqlServer(connectionString);
            });

            return services;
        }
        #endregion
    }
}
