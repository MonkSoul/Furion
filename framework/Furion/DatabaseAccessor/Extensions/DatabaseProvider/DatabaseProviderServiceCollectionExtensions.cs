// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.DatabaseAccessor;
using Furion.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Concurrent;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Sqlite 数据库服务拓展
/// </summary>
[SuppressSniffer]
public static class DatabaseProviderServiceCollectionExtensions
{
    /// <summary>
    /// 添加默认数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <param name="services">服务</param>
    /// <param name="providerName">数据库提供器</param>
    /// <param name="optionBuilder"></param>
    /// <param name="connectionMetadata">支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或 配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</param>
    /// <param name="poolSize">池大小</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDbPool<TDbContext>(this IServiceCollection services, string providerName = default, Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder = null, string connectionMetadata = default, int poolSize = 100, params IInterceptor[] interceptors)
        where TDbContext : DbContext
    {
        // 注册数据库上下文
        return services.AddDbPool<TDbContext, MasterDbContextLocator>(providerName, optionBuilder, connectionMetadata, poolSize, interceptors);
    }

    /// <summary>
    /// 添加默认数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <param name="services">服务</param>
    /// <param name="optionBuilder">自定义配置</param>
    /// <param name="poolSize">池大小</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDbPool<TDbContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder, int poolSize = 100, params IInterceptor[] interceptors)
        where TDbContext : DbContext
    {
        // 注册数据库上下文
        return services.AddDbPool<TDbContext, MasterDbContextLocator>(optionBuilder, poolSize, interceptors);
    }

    /// <summary>
    /// 添加其他数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="services">服务</param>
    /// <param name="providerName">数据库提供器</param>
    /// <param name="optionBuilder"></param>
    /// <param name="connectionMetadata">支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或 配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</param>
    /// <param name="poolSize">池大小</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDbPool<TDbContext, TDbContextLocator>(this IServiceCollection services, string providerName = default, Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder = null, string connectionMetadata = default, int poolSize = 100, params IInterceptor[] interceptors)
        where TDbContext : DbContext
        where TDbContextLocator : class, IDbContextLocator
    {
        // 注册数据库上下文
        services.RegisterDbContext<TDbContext, TDbContextLocator>();

        // 配置数据库上下文
        var connStr = DbProvider.GetConnectionString<TDbContext>(connectionMetadata);
        services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext((serviceProvider, options) =>
        {
            var _options = ConfigureDatabase<TDbContext>(providerName, connStr, options);
            optionBuilder?.Invoke(serviceProvider, _options);
        }, interceptors), poolSize: poolSize);

        return services;
    }

    /// <summary>
    /// 添加其他数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="services">服务</param>
    /// <param name="optionBuilder">自定义配置</param>
    /// <param name="poolSize">池大小</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDbPool<TDbContext, TDbContextLocator>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder, int poolSize = 100, params IInterceptor[] interceptors)
        where TDbContext : DbContext
        where TDbContextLocator : class, IDbContextLocator
    {
        // 注册数据库上下文
        services.RegisterDbContext<TDbContext, TDbContextLocator>();

        // 配置数据库上下文
        services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext(optionBuilder, interceptors), poolSize: poolSize);

        return services;
    }

    /// <summary>
    ///  添加默认数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <param name="services">服务</param>
    /// <param name="providerName">数据库提供器</param>
    /// <param name="optionBuilder"></param>
    /// <param name="connectionMetadata">支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或 配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDb<TDbContext>(this IServiceCollection services, string providerName = default, Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder = null, string connectionMetadata = default, params IInterceptor[] interceptors)
        where TDbContext : DbContext
    {
        // 注册数据库上下文
        return services.AddDb<TDbContext, MasterDbContextLocator>(providerName, optionBuilder, connectionMetadata, interceptors);
    }

    /// <summary>
    ///  添加默认数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <param name="services">服务</param>
    /// <param name="optionBuilder">自定义配置</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDb<TDbContext>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
        where TDbContext : DbContext
    {
        // 注册数据库上下文
        return services.AddDb<TDbContext, MasterDbContextLocator>(optionBuilder, interceptors);
    }

    /// <summary>
    /// 添加数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="services">服务</param>
    /// <param name="providerName">数据库提供器</param>
    /// <param name="optionBuilder"></param>
    /// <param name="connectionMetadata">支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或 配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDb<TDbContext, TDbContextLocator>(this IServiceCollection services, string providerName = default, Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder = null, string connectionMetadata = default, params IInterceptor[] interceptors)
        where TDbContext : DbContext
        where TDbContextLocator : class, IDbContextLocator
    {
        // 注册数据库上下文
        services.RegisterDbContext<TDbContext, TDbContextLocator>();

        // 配置数据库上下文
        var connStr = DbProvider.GetConnectionString<TDbContext>(connectionMetadata);
        services.AddDbContext<TDbContext>(Penetrates.ConfigureDbContext((serviceProvider, options) =>
        {
            var _options = ConfigureDatabase<TDbContext>(providerName, connStr, options);
            optionBuilder?.Invoke(serviceProvider, _options);
        }, interceptors));

        return services;
    }

    /// <summary>
    /// 添加数据库上下文
    /// </summary>
    /// <typeparam name="TDbContext">数据库上下文</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="services">服务</param>
    /// <param name="optionBuilder">自定义配置</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddDb<TDbContext, TDbContextLocator>(this IServiceCollection services, Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
        where TDbContext : DbContext
        where TDbContextLocator : class, IDbContextLocator
    {
        // 注册数据库上下文
        services.RegisterDbContext<TDbContext, TDbContextLocator>();

        // 配置数据库上下文
        services.AddDbContext<TDbContext>(Penetrates.ConfigureDbContext(optionBuilder, interceptors));

        return services;
    }

    /// <summary>
    /// 配置数据库
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="providerName">数据库提供器</param>
    /// <param name="connectionMetadata">支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或 配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</param>
    /// <param name="options">数据库上下文选项构建器</param>
    private static DbContextOptionsBuilder ConfigureDatabase<TDbContext>(string providerName, string connectionMetadata, DbContextOptionsBuilder options)
         where TDbContext : DbContext
    {
        var dbContextOptionsBuilder = options;

        // 获取数据库上下文特性
        var dbContextAttribute = DbProvider.GetAppDbContextAttribute(typeof(TDbContext));
        if (!string.IsNullOrWhiteSpace(connectionMetadata))
        {
            providerName ??= dbContextAttribute?.ProviderName;

            // 解析数据库提供器信息
            (var name, var version) = ReadProviderInfo(providerName);
            providerName = name;

            // 调用对应数据库程序集
            var (UseMethod, MySqlVersion) = GetDatabaseProviderUseMethod(providerName, version);

            // 处理最新第三方 MySql 包兼容问题
            // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
            if (DbProvider.IsDatabaseFor(providerName, DbProvider.MySql))
            {
                dbContextOptionsBuilder = UseMethod
                    .Invoke(null, new object[] { options, connectionMetadata, MySqlVersion, MigrationsAssemblyAction }) as DbContextOptionsBuilder;
            }
            // 处理 SqlServer 2005-2008 兼容问题
            else if (DbProvider.IsDatabaseFor(providerName, DbProvider.SqlServer) && (version == "2008" || version == "2005"))
            {
                // 替换工厂
                dbContextOptionsBuilder.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();

                dbContextOptionsBuilder = UseMethod
                    .Invoke(null, new object[] { options, connectionMetadata, MigrationsAssemblyAction }) as DbContextOptionsBuilder;
            }
            // 处理 Oracle 11 兼容问题
            else if (DbProvider.IsDatabaseFor(providerName, DbProvider.Oracle) && !string.IsNullOrWhiteSpace(version))
            {
                Action<IRelationalDbContextOptionsBuilderInfrastructure> oracleOptionsAction = options =>
                {
                    var optionsType = options.GetType();

                    // 处理版本号
                    optionsType.GetMethod("UseOracleSQLCompatibility")
                           .Invoke(options, new[] { version });

                    // 处理迁移程序集
                    optionsType.GetMethod("MigrationsAssembly")
                           .Invoke(options, new[] { Db.MigrationAssemblyName });
                };

                dbContextOptionsBuilder = UseMethod
                    .Invoke(null, new object[] { options, connectionMetadata, oracleOptionsAction }) as DbContextOptionsBuilder;
            }
            // 处理内存数据库
            else if (DbProvider.IsDatabaseFor(providerName, DbProvider.InMemoryDatabase))
            {
                dbContextOptionsBuilder = UseMethod
                    .Invoke(null, new object[] { options, connectionMetadata, null }) as DbContextOptionsBuilder;
            }
            else
            {
                dbContextOptionsBuilder = UseMethod
                    .Invoke(null, new object[] { options, connectionMetadata, MigrationsAssemblyAction }) as DbContextOptionsBuilder;
            }
        }

        // 解决分表分库
        if (dbContextAttribute?.Mode == DbContextMode.Dynamic) dbContextOptionsBuilder
              .ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();

        return dbContextOptionsBuilder;
    }

    /// <summary>
    /// 数据库提供器 UseXXX 方法缓存集合
    /// </summary>
    private static readonly ConcurrentDictionary<string, (MethodInfo, object)> DatabaseProviderUseMethodCollection;

    /// <summary>
    /// 配置Code First 程序集 Action委托
    /// </summary>
    private static readonly Action<IRelationalDbContextOptionsBuilderInfrastructure> MigrationsAssemblyAction;

    /// <summary>
    /// 静态构造方法
    /// </summary>
    static DatabaseProviderServiceCollectionExtensions()
    {
        DatabaseProviderUseMethodCollection = new ConcurrentDictionary<string, (MethodInfo, object)>();
        MigrationsAssemblyAction = options =>
        {
            var optionsType = options.GetType();

            optionsType.GetMethod("MigrationsAssembly")
                   .Invoke(options, new[] { Db.MigrationAssemblyName });

            // 解决 MySQL/SqlServer/PostgreSQL 有时候出现短暂连接失败问题（v4.8.1.7 版本关闭）
            // https://learn.microsoft.com/zh-cn/ef/core/miscellaneous/connection-resiliency
            //var enableRetryOnFailureMethod = optionsType.GetMethod("EnableRetryOnFailure", new[]
            //{
            //    typeof(int),typeof(TimeSpan),typeof(IEnumerable<int>)
            //});

            //enableRetryOnFailureMethod?.Invoke(options, new object[]
            //{
            //    5,TimeSpan.FromSeconds(30),new int[] { 2 }
            //});
        };
    }

    /// <summary>
    /// 获取数据库提供器对应的 useXXX 方法
    /// </summary>
    /// <param name="providerName">数据库提供器</param>
    /// <param name="version"></param>
    /// <returns></returns>
    private static (MethodInfo UseMethod, object MySqlVersion) GetDatabaseProviderUseMethod(string providerName, string version)
    {
        return DatabaseProviderUseMethodCollection.GetOrAdd(providerName, Function(providerName, version));

        // 本地静态方法
        static (MethodInfo, object) Function(string providerName, string version)
        {
            // 处理最新 MySql 包兼容问题
            // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
            object mySqlVersionInstance = default;

            // 加载对应的数据库提供器程序集
            var databaseProviderAssembly = Reflect.GetAssembly(providerName);

            // 数据库提供器服务拓展类型名
            var databaseProviderServiceExtensionTypeName = providerName switch
            {
                DbProvider.SqlServer => "SqlServerDbContextOptionsExtensions",
                DbProvider.Sqlite => "SqliteDbContextOptionsBuilderExtensions",
                DbProvider.Cosmos => "CosmosDbContextOptionsExtensions",
                DbProvider.InMemoryDatabase => "InMemoryDbContextOptionsExtensions",
                DbProvider.MySql => "MySqlDbContextOptionsBuilderExtensions",
                DbProvider.MySqlOfficial => "MySQLDbContextOptionsExtensions",
                DbProvider.Npgsql => "NpgsqlDbContextOptionsBuilderExtensions",
                DbProvider.Oracle => "OracleDbContextOptionsExtensions",
                DbProvider.Firebird => "FbDbContextOptionsBuilderExtensions",
                DbProvider.Dm => "DmDbContextOptionsExtensions",
                _ => null
            };

            // 加载拓展类型
            var databaseProviderServiceExtensionType = Reflect.GetType(databaseProviderAssembly, $"Microsoft.EntityFrameworkCore.{databaseProviderServiceExtensionTypeName}");

            // useXXX方法名
            var useMethodName = providerName switch
            {
                DbProvider.SqlServer => $"Use{nameof(DbProvider.SqlServer)}",
                DbProvider.Sqlite => $"Use{nameof(DbProvider.Sqlite)}",
                DbProvider.Cosmos => $"Use{nameof(DbProvider.Cosmos)}",
                DbProvider.InMemoryDatabase => $"Use{nameof(DbProvider.InMemoryDatabase)}",
                DbProvider.MySql => $"Use{nameof(DbProvider.MySql)}",
                DbProvider.MySqlOfficial => $"UseMySQL",
                DbProvider.Npgsql => $"Use{nameof(DbProvider.Npgsql)}",
                DbProvider.Oracle => $"Use{nameof(DbProvider.Oracle)}",
                DbProvider.Firebird => $"Use{nameof(DbProvider.Firebird)}",
                DbProvider.Dm => $"Use{nameof(DbProvider.Dm)}",
                _ => null
            };

            // 获取UseXXX方法
            MethodInfo useMethod;

            // 处理最新 MySql 第三方包兼容问题
            // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
            if (DbProvider.IsDatabaseFor(providerName, DbProvider.MySql))
            {
                useMethod = databaseProviderServiceExtensionType
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(u => u.Name == useMethodName && !u.IsGenericMethod && u.GetParameters().Length == 4 && u.GetParameters()[1].ParameterType == typeof(string));

                // 解析mysql版本类型
                var mysqlVersionType = Reflect.GetType(databaseProviderAssembly, "Microsoft.EntityFrameworkCore.MySqlServerVersion");
                mySqlVersionInstance = Activator.CreateInstance(mysqlVersionType, new object[] { new Version(version ?? "8.0.22") });
            }
            else
            {
                useMethod = databaseProviderServiceExtensionType
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(u => u.Name == useMethodName && !u.IsGenericMethod && u.GetParameters().Length == 3 && u.GetParameters()[1].ParameterType == typeof(string));
            }

            return (useMethod, mySqlVersionInstance);
        }
    }

    /// <summary>
    /// 解析数据库提供器信息
    /// </summary>
    /// <param name="providerName"></param>
    /// <returns></returns>
    private static (string name, string version) ReadProviderInfo(string providerName)
    {
        // 解析真实的数据库提供器
        var providerNameAndVersion = providerName.Split('@', StringSplitOptions.RemoveEmptyEntries);
        providerName = providerNameAndVersion.First();

        var providerVersion = providerNameAndVersion.Length > 1 ? providerNameAndVersion[1] : default;
        return (providerName, providerVersion);
    }
}