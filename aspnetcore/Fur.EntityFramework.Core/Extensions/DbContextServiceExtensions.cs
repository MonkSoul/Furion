using Fur.EntityFramework.Core.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fur.EntityFramework.Core.Extensions
{
    /// <summary>
    /// 数据库操作上下文服务拓展类
    /// </summary>
    public static class DbContextServiceExtensions
    {
        #region 数据库上下文服务拓展方法 +/* public static IServiceCollection AddFurDbContextPool(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        /// <summary>
        /// 数据库上下文服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="env">环境对象</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的服务集合对象</returns>
        public static IServiceCollection AddFurDbContextPool(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            services.AddDbContextPool<FurSqlServerDbContext>(options =>
            {
                if (env.IsDevelopment())
                {
                    options/*.UseLazyLoadingProxies()*/
                                .EnableDetailedErrors()
                                .EnableSensitiveDataLogging();
                }
                options.UseSqlServer(configuration.GetConnectionString("FurConnectionString"));
            }
            , poolSize: 128);

            return services;
        }
        #endregion
    }
}
