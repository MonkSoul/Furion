using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// MongoDB 拓展类
    /// </summary>
    public static class MongoDBServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 MongoDB 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDB(this IServiceCollection services, string connectionString)
        {
            // 创建数据库连接对象
            services.AddScoped<IMongoClient>(u =>
            {
                return new MongoClient(connectionString);
            });

            // 注册 MongoDB 仓储
            services.AddScoped<IMongoDBRepository, MongoDBRepository>();

            return services;
        }

        /// <summary>
        /// 添加 MongoDB 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDB(this IServiceCollection services, MongoClientSettings settings)
        {
            // 创建数据库连接对象
            services.AddScoped<IMongoClient>(u =>
            {
                return new MongoClient(settings);
            });

            // 注册 MongoDB 仓储
            services.AddScoped<IMongoDBRepository, MongoDBRepository>();

            return services;
        }

        /// <summary>
        /// 添加 MongoDB 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDB(this IServiceCollection services, MongoUrl url)
        {
            // 创建数据库连接对象
            services.AddScoped<IMongoClient>(u =>
            {
                return new MongoClient(url);
            });

            // 注册 MongoDB 仓储
            services.AddScoped<IMongoDBRepository, MongoDBRepository>();

            return services;
        }
    }
}