using Fur;
using Fur.DatabaseAccessor;
using Fur.DatabaseAccessor.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 数据库访问器服务拓展类
    /// </summary>
    public static class DatabaseAccessorServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库访问器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseAccessor(this IServiceCollection services)
        {
            // 注册数据库上下文池
            services.AddScoped<IDbContextPool, DbContextPool>();

            // 注册泛型仓储
            services.AddScoped(typeof(IRepository<>), typeof(EFCoreRepository<>));

            return services;
        }

        public static IServiceCollection AddAppDbContext<TDbContext>(this IServiceCollection services, string connectionString, int poolSize = 100)
            where TDbContext : DbContext
        {
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
    }
}