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

using Furion.Templates.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Data;
using System.Reflection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库提供器选项
/// </summary>
[SuppressSniffer]
public static class DbProvider
{
    /// <summary>
    /// SqlServer 提供器程序集
    /// </summary>
    public const string SqlServer = "Microsoft.EntityFrameworkCore.SqlServer";

    /// <summary>
    /// Sqlite 提供器程序集
    /// </summary>
    public const string Sqlite = "Microsoft.EntityFrameworkCore.Sqlite";

    /// <summary>
    /// Cosmos 提供器程序集
    /// </summary>
    public const string Cosmos = "Microsoft.EntityFrameworkCore.Cosmos";

    /// <summary>
    /// 内存数据库 提供器程序集
    /// </summary>
    public const string InMemoryDatabase = "Microsoft.EntityFrameworkCore.InMemory";

    /// <summary>
    /// MySql 提供器程序集
    /// </summary>
    public const string MySql = "Pomelo.EntityFrameworkCore.MySql";

    /// <summary>
    /// MySql 官方包（更新不及时，只支持 8.0.23+ 版本， 所以单独弄一个分类）
    /// </summary>
    public const string MySqlOfficial = "MySql.EntityFrameworkCore";

    /// <summary>
    /// PostgreSQL 提供器程序集
    /// </summary>
    public const string Npgsql = "Npgsql.EntityFrameworkCore.PostgreSQL";

    /// <summary>
    /// Oracle 提供器程序集
    /// </summary>
    public const string Oracle = "Oracle.EntityFrameworkCore";

    /// <summary>
    /// Firebird 提供器程序集
    /// </summary>
    public const string Firebird = "FirebirdSql.EntityFrameworkCore.Firebird";

    /// <summary>
    /// Dm 提供器程序集
    /// </summary>
    public const string Dm = "Microsoft.EntityFrameworkCore.Dm";

    /// <summary>
    /// 不支持存储过程的数据库
    /// </summary>
    internal static readonly string[] NotSupportStoredProcedureDatabases;

    /// <summary>
    /// 不支持函数的数据库
    /// </summary>
    internal static readonly string[] NotSupportFunctionDatabases;

    /// <summary>
    /// 不支持表值函数的数据库
    /// </summary>
    internal static readonly string[] NotSupportTableFunctionDatabases;

    /// <summary>
    /// 构造函数
    /// </summary>
    static DbProvider()
    {
        NotSupportStoredProcedureDatabases = new[]
        {
                Sqlite,
                InMemoryDatabase
            };

        NotSupportFunctionDatabases = new[]
        {
                Sqlite,
                InMemoryDatabase,
                Firebird,
                Dm,
            };

        NotSupportTableFunctionDatabases = new[]
        {
                Sqlite,
                InMemoryDatabase,
                MySql,
                MySqlOfficial,
                Firebird,
                Dm
            };

        DbContextAppDbContextAttributes = new ConcurrentDictionary<Type, AppDbContextAttribute>();
    }

    /// <summary>
    /// 判断是否是特定数据库
    /// </summary>
    /// <param name="providerName"></param>
    /// <param name="dbAssemblyName"></param>
    /// <returns>bool</returns>
    public static bool IsDatabaseFor(string providerName, string dbAssemblyName)
    {
        return providerName.Equals(dbAssemblyName, StringComparison.Ordinal);
    }

    /// <summary>
    /// 获取数据库上下文连接字符串
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="connectionMetadata">支持数据库连接字符串，配置文件的 ConnectionStrings 中的Key或 配置文件的完整的配置路径，如果是内存数据库，则为数据库名称</param>
    /// <returns></returns>
    public static string GetConnectionString<TDbContext>(string connectionMetadata = default)
        where TDbContext : DbContext
    {
        // 支持读取配置渲染
        var connStrOrConnKey = connectionMetadata.Render();

        // 如果没有配置数据库连接字符串，那么查找特性
        if (string.IsNullOrWhiteSpace(connStrOrConnKey))
        {
            var dbContextAttribute = GetAppDbContextAttribute(typeof(TDbContext));
            if (dbContextAttribute == null) return default;

            // 获取特性连接字符串（渲染配置模板）
            connStrOrConnKey = dbContextAttribute.ConnectionMetadata.Render();
        }

        // 如果都没有，则直接返回空
        if (string.IsNullOrWhiteSpace(connStrOrConnKey)) return default;

        // 如果包含 = 符号，那么认为是连接字符串
        if (connStrOrConnKey.Contains('=')) return connStrOrConnKey;
        else
        {
            var configuration = App.Configuration;

            // 如果包含 : 符号，那么认为是一个 Key 路径
            if (connStrOrConnKey.Contains(':')) return configuration[connStrOrConnKey];
            else
            {
                // 首先查找 DbConnectionString 键，如果没有找到，则当成 Key 去查找
                var connStrValue = configuration.GetConnectionString(connStrOrConnKey);
                return (!string.IsNullOrWhiteSpace(connStrValue) ? connStrValue : configuration[connStrOrConnKey]) ?? connStrOrConnKey;
            }
        }
    }

    /// <summary>
    /// 获取默认拦截器
    /// </summary>
    public static List<IInterceptor> GetDefaultInterceptors()
    {
        return new List<IInterceptor>
            {
                new SqlConnectionProfilerInterceptor(),
                new SqlCommandProfilerInterceptor(),
                new DbContextSaveChangesInterceptor()
            };
    }

    /// <summary>
    /// 数据库上下文 [AppDbContext] 特性缓存
    /// </summary>
    private static readonly ConcurrentDictionary<Type, AppDbContextAttribute> DbContextAppDbContextAttributes;

    /// <summary>
    /// 获取数据库上下文 [AppDbContext] 特性
    /// </summary>
    /// <param name="dbContexType"></param>
    /// <returns></returns>
    internal static AppDbContextAttribute GetAppDbContextAttribute(Type dbContexType)
    {
        return DbContextAppDbContextAttributes.GetOrAdd(dbContexType, Function);

        // 本地静态函数
        static AppDbContextAttribute Function(Type dbContextType)
        {
            if (!dbContextType.IsDefined(typeof(AppDbContextAttribute), true)) return default;

            var appDbContextAttribute = dbContextType.GetCustomAttribute<AppDbContextAttribute>(true);

            return appDbContextAttribute;
        }
    }

    /// <summary>
    /// 不支持操作类型
    /// </summary>
    private const string NotSupportException = "The database provider does not support {0} operations.";

    /// <summary>
    /// 检查是否支持存储过程
    /// </summary>
    /// <param name="providerName">数据库提供器名词</param>
    /// <param name="commandType">命令类型</param>
    internal static void CheckStoredProcedureSupported(string providerName, CommandType commandType)
    {
        if (commandType == CommandType.StoredProcedure && NotSupportStoredProcedureDatabases.Contains(providerName))
        {
            throw new NotSupportedException(string.Format(NotSupportException, "stored procedure"));
        }
    }

    /// <summary>
    /// 检查是否支持函数
    /// </summary>
    /// <param name="providerName">数据库提供器名</param>
    /// <param name="dbFunctionType">数据库函数类型</param>
    internal static void CheckFunctionSupported(string providerName, DbFunctionType dbFunctionType)
    {
        if (NotSupportFunctionDatabases.Contains(providerName))
        {
            throw new NotSupportedException(string.Format(NotSupportException, "function"));
        }

        if (dbFunctionType == DbFunctionType.Table && NotSupportTableFunctionDatabases.Contains(providerName))
        {
            throw new NotSupportedException(string.Format(NotSupportException, "table function"));
        }
    }
}