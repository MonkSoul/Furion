using Fur.DatabaseAccessor.Filters;
using Fur.EntityFramework.Core.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 数据库操作上下文服务拓展类
    /// </summary>
    public static class DbContextServiceCollectionExtensions
    {
        /// <summary>
        /// 数据库上下文服务拓展方法
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="env">环境对象</param>
        /// <param name="configuration">配置选项</param>
        /// <returns>新的服务集合对象</returns>
        public static IServiceCollection AddFurDbContextPool(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var env = serviceProvider.GetService<IWebHostEnvironment>();

            services.AddFurSqlServerDbContextPool<FurSqlServerDbContext>(configuration.GetConnectionString("FurConnectionString"), env);
            services.Configure<MvcOptions>(options => options.Filters.Add<UnitOfWorkAsyncActionFilter>());

            return services;
        }
    }
}