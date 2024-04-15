// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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