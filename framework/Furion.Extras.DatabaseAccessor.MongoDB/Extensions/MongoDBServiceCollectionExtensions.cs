// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// MongoDB 拓展类
/// </summary>
public static class MongoDBServiceCollectionExtensions
{
    private const string defaultDbName = "furion";

    /// <summary>
    /// 添加 MongoDB 拓展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddMongoDB(this IServiceCollection services, string connectionString)
    {
        // 创建数据库连接对象
        services.AddScoped<IMongoDatabase>(u =>
        {
            var mongoUrl = new MongoUrl(connectionString);
            var dbName = mongoUrl.DatabaseName ?? defaultDbName;
            return new MongoClient(connectionString).GetDatabase(dbName);
        });

        // 注册 MongoDB 仓储
        services.AddScoped<IMongoDBRepository, MongoDBRepository>();
        services.AddScoped(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
        services.AddScoped(typeof(IMongoDBRepository<,>), typeof(MongoDBRepository<,>));

        return services;
    }

    /// <summary>
    /// 添加 MongoDB 拓展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="settings"></param>
    /// <param name="dbName">数据库名称</param>
    /// <returns></returns>
    public static IServiceCollection AddMongoDB(this IServiceCollection services, MongoClientSettings settings, string dbName = "furion")
    {
        // 创建数据库连接对象
        services.AddScoped<IMongoDatabase>(u =>
        {
            return new MongoClient(settings).GetDatabase(dbName);
        });

        // 注册 MongoDB 仓储
        services.AddScoped<IMongoDBRepository, MongoDBRepository>();
        services.AddScoped(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
        services.AddScoped(typeof(IMongoDBRepository<,>), typeof(MongoDBRepository<,>));

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
        services.AddScoped<IMongoDatabase>(u =>
        {
            var dbName = url.DatabaseName ?? defaultDbName;
            return new MongoClient(url).GetDatabase(dbName);
        });

        // 注册 MongoDB 仓储
        services.AddScoped<IMongoDBRepository, MongoDBRepository>();
        services.AddScoped(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
        services.AddScoped(typeof(IMongoDBRepository<,>), typeof(MongoDBRepository<,>));

        return services;
    }
}