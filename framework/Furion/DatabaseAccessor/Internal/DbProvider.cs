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