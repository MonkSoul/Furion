// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.8.5
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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