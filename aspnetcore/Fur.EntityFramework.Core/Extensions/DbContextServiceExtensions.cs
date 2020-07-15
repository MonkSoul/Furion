using Fur.DatabaseVisitor.Contexts;
using Fur.DatabaseVisitor.Extensions.ServiceCollection;
using Fur.DatabaseVisitor.Filters;
using Fur.EntityFramework.Core.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fur.EntityFramework.Core.Extensions
{
    /// <summary>
    /// 数据库操作上下文服务拓展类
    /// </summary>
    public static class DbContextServiceExtensions
    {
        #region 数据库上下文服务拓展方法 + public static IServiceCollection AddFurDbContextPool(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        /// <summary>
        /// 数据库上下文服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="env">环境对象</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的服务集合对象</returns>
        public static IServiceCollection AddFurDbContextPool(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
        {
            var furConnectionString = configuration.GetConnectionString("FurConnectionString");
            var furMultipleConnectionString = configuration.GetConnectionString("FurMultipleConnectionString");

            services.AddFurSqlServerDbContextPool<FurSqlServerDbContext>(furConnectionString, env)
                        .AddFurSqlServerDbContextPool<FurMultipleSqlServerDbContext>(furMultipleConnectionString, env)
                        .AddFurSqlServerDbContextPool<FurTenantDbContext>(furConnectionString, env);

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<UnitOfWorkAsyncActionFilter>();
            });

            return services;
        }
        #endregion 数据库上下文服务拓展方法 + public static IServiceCollection AddFurDbContextPool(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    }
}