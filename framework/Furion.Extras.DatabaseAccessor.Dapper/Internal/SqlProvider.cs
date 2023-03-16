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