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

using System.Collections.Concurrent;
using System.Reflection;

namespace Dapper;

/// <summary>
/// Sql 类型
/// </summary>
public static class SqlProvider
{
    /// <summary>
    /// SqlServer 提供器程序集
    /// </summary>
    public const string SqlServer = "Microsoft.Data.SqlClient";

    /// <summary>
    /// Sqlite 提供器程序集
    /// </summary>
    public const string Sqlite = "Microsoft.Data.Sqlite";

    /// <summary>
    /// MySql 提供器程序集
    /// </summary>
    public const string MySql = "MySql.Data";

    /// <summary>
    /// PostgreSQL 提供器程序集
    /// </summary>
    public const string Npgsql = "Npgsql";

    /// <summary>
    /// Oracle 提供器程序集
    /// </summary>
    public const string Oracle = "Oracle.ManagedDataAccess";

    /// <summary>
    /// Firebird 提供器程序集
    /// </summary>
    public const string Firebird = "FirebirdSql.Data.FirebirdClient";

    /// <summary>
    /// 数据库提供器连接对象类型集合
    /// </summary>
    internal static readonly ConcurrentDictionary<string, Type> SqlProviderDbConnectionTypeCollection;

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static SqlProvider()
    {
        SqlProviderDbConnectionTypeCollection = new ConcurrentDictionary<string, Type>();
    }

    /// <summary>
    /// 获取数据库连接对象类型
    /// </summary>
    /// <param name="sqlProvider"></param>
    /// <returns></returns>
    internal static Type GetDbConnectionType(string sqlProvider)
    {
        return SqlProviderDbConnectionTypeCollection.GetOrAdd(sqlProvider, Function);

        // 本地静态方法
        static Type Function(string sqlProvider)
        {
            // 加载对应的数据库提供器程序集
            var databaseProviderAssembly = Assembly.Load(sqlProvider);

            // 获取对应数据库连接对象
            var databaseDbConnectionTypeName = sqlProvider switch
            {
                SqlServer => "Microsoft.Data.SqlClient.SqlConnection",
                Sqlite => "Microsoft.Data.Sqlite.SqliteConnection",
                MySql => "MySql.Data.MySqlClient.MySqlConnection",
                Npgsql => "Npgsql.NpgsqlConnection",
                Oracle => "Oracle.ManagedDataAccess.Client.OracleConnection",
                Firebird => "FirebirdSql.Data.FirebirdClient.FbConnection",
                _ => null
            };

            // 加载数据库连接对象类型
            var dbConnectionType = databaseProviderAssembly.GetType(databaseDbConnectionTypeName);

            return dbConnectionType;
        }
    }
}