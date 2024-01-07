// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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