using Dapper;
using System;
using System.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Dapper 拓展类
    /// </summary>
    public static class DapperServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 Dapper 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="sqlProvider"> <see cref="Dapper.SqlProvider"/> 类型</param>
        /// <returns></returns>
        public static IServiceCollection AddDapper(this IServiceCollection services, string connectionString, string sqlProvider)
        {
            // 获取数据库类型
            var dbConnectionType = SqlProvider.GetDbConnectionType(sqlProvider);

            // 创建数据库连接对象
            services.AddScoped(u =>
            {
                var dbConnection = Activator.CreateInstance(dbConnectionType, new[] { connectionString }) as IDbConnection;
                if (dbConnection.State != ConnectionState.Open) dbConnection.Open();

                return dbConnection;
            });

            // 注册非泛型仓储
            services.AddScoped<IDapperRepository, DapperRepository>();

            // 注册 Dapper 仓储
            services.AddScoped(typeof(IDapperRepository<>), typeof(DapperRepository<>));

            return services;
        }
    }
}