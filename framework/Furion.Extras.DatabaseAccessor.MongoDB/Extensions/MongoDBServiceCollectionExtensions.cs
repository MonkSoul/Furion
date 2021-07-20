// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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