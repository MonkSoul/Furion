using FreeSql;
using Furion.DatabaseAccessor;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// FreeSql 服务拓展类
    /// </summary>
    public static class FreeSqlServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 FreeSql 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="dataType"></param>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFreeSql(this IServiceCollection services, string connectionString, DataType dataType, Action<FreeSqlBuilder> buildAction = default)
        {
            return services.AddFreeSql(freeSqlBuilder =>
            {
                freeSqlBuilder.UseConnectionString(dataType, connectionString);
                buildAction?.Invoke(freeSqlBuilder);
            });
        }

        /// <summary>
        /// 添加 FreeSql 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddFreeSql(this IServiceCollection services, Action<FreeSqlBuilder> buildAction = default)
        {
            services.AddSingleton(u =>
            {
                var freeSqlBuilder = new FreeSqlBuilder();

                buildAction?.Invoke(freeSqlBuilder);

                return freeSqlBuilder.Build();
            });

            // 注册非泛型仓储
            services.AddScoped<IFreeSqlRepository, FreeSqlRepository>();

            // 注册 SqlSugar 仓储
            services.AddScoped(typeof(IFreeSqlRepository<>), typeof(FreeSqlRepository<>));

            return services;
        }
    }
}