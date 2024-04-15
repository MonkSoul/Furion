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

using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Data.Common;

namespace Furion.DatabaseAccessor;

/// <summary>
/// ADONET 拓展类
/// </summary>
[SuppressSniffer]
public static class SqlAdoNetExtensions
{
    /// <summary>
    /// 执行 Sql 返回 DataTable
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="behavior">行为</param>
    /// <returns>DataTable</returns>
    public static DataTable ExecuteReader(this DatabaseFacade databaseFacade, string sql, DbParameter[] parameters = null, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand) = databaseFacade.PrepareDbCommand(sql, parameters, commandType);

        // 读取数据
        using var dbDataReader = dbCommand.ExecuteReader(behavior);

        // 填充到 DataTable
        var dataTable = dbDataReader.ToDataTable();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        dbCommand.Dispose();

        return dataTable;
    }

    /// <summary>
    /// 执行 Sql 返回 DataTable
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="model">命令模型</param>
    /// <param name="behavior">行为</param>
    /// <returns>(DataTable dataTable, DbParameter[] dbParameters)</returns>
    public static (DataTable dataTable, DbParameter[] dbParameters) ExecuteReader(this DatabaseFacade databaseFacade, string sql, object model, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand, dbParameters) = databaseFacade.PrepareDbCommand(sql, model, commandType);

        // 读取数据
        using var dbDataReader = dbCommand.ExecuteReader(behavior);

        // 填充到 DataTable
        var dataTable = dbDataReader.ToDataTable();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        dbCommand.Dispose();

        return (dataTable, dbParameters);
    }

    /// <summary>
    /// 执行 Sql 返回 DataTable
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="behavior">行为</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>DataTable</returns>
    public static async Task<DataTable> ExecuteReaderAsync(this DatabaseFacade databaseFacade, string sql, DbParameter[] parameters = null, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, parameters, commandType, cancellationToken);

        // 读取数据
        using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

        // 填充到 DataTable
        var dataTable = dbDataReader.ToDataTable();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        await dbCommand.DisposeAsync();

        return dataTable;
    }

    /// <summary>
    /// 执行 Sql 返回 DataTable
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">命令模型</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="behavior">行为</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>(DataTable dataTable, DbParameter[] dbParameters)</returns>
    public static async Task<(DataTable dataTable, DbParameter[] dbParameters)> ExecuteReaderAsync(this DatabaseFacade databaseFacade, string sql, object model, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand, dbParameters) = await databaseFacade.PrepareDbCommandAsync(sql, model, commandType, cancellationToken);

        // 读取数据
        using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

        // 填充到 DataTable
        var dataTable = dbDataReader.ToDataTable();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        await dbCommand.DisposeAsync();

        return (dataTable, dbParameters);
    }

    /// <summary>
    /// 执行 Sql 语句返回受影响行数
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="commandType">命令类型</param>
    /// <returns>受影响行数</returns>
    public static int ExecuteNonQuery(this DatabaseFacade databaseFacade, string sql, DbParameter[] parameters = null, CommandType commandType = CommandType.Text)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand) = databaseFacade.PrepareDbCommand(sql, parameters, commandType);

        // 执行返回受影响行数
        var rowEffects = dbCommand.ExecuteNonQuery();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        dbCommand.Dispose();

        return rowEffects;
    }

    /// <summary>
    /// 执行 Sql 语句返回受影响行数
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="commandType">命令类型</param>
    /// <returns>(int rowEffects, DbParameter[] dbParameters)</returns>
    public static (int rowEffects, DbParameter[] dbParameters) ExecuteNonQuery(this DatabaseFacade databaseFacade, string sql, object model, CommandType commandType = CommandType.Text)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand, dbParameters) = databaseFacade.PrepareDbCommand(sql, model, commandType);

        // 执行返回受影响行数
        var rowEffects = dbCommand.ExecuteNonQuery();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        dbCommand.Dispose();

        return (rowEffects, dbParameters);
    }

    /// <summary>
    /// 执行 Sql 语句返回受影响行数
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>受影响行数</returns>
    public static async Task<int> ExecuteNonQueryAsync(this DatabaseFacade databaseFacade, string sql, DbParameter[] parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, parameters, commandType, cancellationToken);

        // 执行返回受影响行数
        var rowEffects = await dbCommand.ExecuteNonQueryAsync(cancellationToken);

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        await dbCommand.DisposeAsync();

        return rowEffects;
    }

    /// <summary>
    /// 执行 Sql 语句返回受影响行数
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">命令模型</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>(int rowEffects, DbParameter[] dbParameters)</returns>
    public static async Task<(int rowEffects, DbParameter[] dbParameters)> ExecuteNonQueryAsync(this DatabaseFacade databaseFacade, string sql, object model, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand, dbParameters) = await databaseFacade.PrepareDbCommandAsync(sql, model, commandType, cancellationToken);

        // 执行返回受影响行数
        var rowEffects = await dbCommand.ExecuteNonQueryAsync(cancellationToken);

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        await dbCommand.DisposeAsync();

        return (rowEffects, dbParameters);
    }

    /// <summary>
    /// 执行 Sql 返回单行单列的值
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="commandType">命令类型</param>
    /// <returns>单行单列的值</returns>
    public static object ExecuteScalar(this DatabaseFacade databaseFacade, string sql, DbParameter[] parameters = null, CommandType commandType = CommandType.Text)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand) = databaseFacade.PrepareDbCommand(sql, parameters, commandType);

        // 执行返回单行单列的值
        var result = dbCommand.ExecuteScalar();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        dbCommand.Dispose();

        return result != DBNull.Value ? result : default;
    }

    /// <summary>
    /// 执行 Sql 返回单行单列的值
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">命令模型</param>
    /// <param name="commandType">命令类型</param>
    /// <returns>(object result, DbParameter[] dbParameters)</returns>
    public static (object result, DbParameter[] dbParameters) ExecuteScalar(this DatabaseFacade databaseFacade, string sql, object model, CommandType commandType = CommandType.Text)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand, dbParameters) = databaseFacade.PrepareDbCommand(sql, model, commandType);

        // 执行返回单行单列的值
        var result = dbCommand.ExecuteScalar();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        dbCommand.Dispose();

        return (result != DBNull.Value ? result : default, dbParameters);
    }

    /// <summary>
    /// 执行 Sql 返回单行单列的值
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>单行单列的值</returns>
    public static async Task<object> ExecuteScalarAsync(this DatabaseFacade databaseFacade, string sql, DbParameter[] parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, parameters, commandType, cancellationToken);

        // 执行返回单行单列的值
        var result = await dbCommand.ExecuteScalarAsync(cancellationToken);

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        await dbCommand.DisposeAsync();

        return result != DBNull.Value ? result : default;
    }

    /// <summary>
    /// 执行 Sql 返回单行单列的值
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">命令模型</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>(object result, DbParameter[] dbParameters)</returns>
    public static async Task<(object result, DbParameter[] dbParameters)> ExecuteScalarAsync(this DatabaseFacade databaseFacade, string sql, object model, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand, dbParameters) = await databaseFacade.PrepareDbCommandAsync(sql, model, commandType, cancellationToken);

        // 执行返回单行单列的值
        var result = await dbCommand.ExecuteScalarAsync(cancellationToken);

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        await dbCommand.DisposeAsync();

        return (result != DBNull.Value ? result : default, dbParameters);
    }

    /// <summary>
    /// 执行 Sql 返回 DataSet
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="behavior">行为</param>
    /// <returns>DataSet</returns>
    public static DataSet DataAdapterFill(this DatabaseFacade databaseFacade, string sql, DbParameter[] parameters = null, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand) = databaseFacade.PrepareDbCommand(sql, parameters, commandType);

        // 读取数据
        using var dbDataReader = dbCommand.ExecuteReader(behavior);

        // 填充到 DataSet
        var dataSet = dbDataReader.ToDataSet();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        dbCommand.Dispose();

        return dataSet;
    }

    /// <summary>
    /// 执行 Sql 返回 DataSet
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="model">命令模型</param>
    /// <param name="behavior">行为</param>
    /// <returns>(DataSet dataSet, DbParameter[] dbParameters)</returns>
    public static (DataSet dataSet, DbParameter[] dbParameters) DataAdapterFill(this DatabaseFacade databaseFacade, string sql, object model, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand, dbParameters) = databaseFacade.PrepareDbCommand(sql, model, commandType);

        // 读取数据
        using var dbDataReader = dbCommand.ExecuteReader(behavior);

        // 填充到 DataSet
        var dataSet = dbDataReader.ToDataSet();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        dbCommand.Dispose();

        return (dataSet, dbParameters);
    }

    /// <summary>
    /// 执行 Sql 返回 DataSet
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="behavior">行为</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>DataSet</returns>
    public static async Task<DataSet> DataAdapterFillAsync(this DatabaseFacade databaseFacade, string sql, DbParameter[] parameters = null, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, parameters, commandType, cancellationToken);

        // 读取数据
        using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

        // 填充到 DataSet
        var dataSet = dbDataReader.ToDataSet();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        await dbCommand.DisposeAsync();

        return dataSet;
    }

    /// <summary>
    /// 执行 Sql 返回 DataSet
    /// </summary>
    /// <param name="databaseFacade">ADO.NET 数据库对象</param>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">命令模型</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="behavior">行为</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>(DataSet dataSet, DbParameter[] dbParameters)</returns>
    public static async Task<(DataSet dataSet, DbParameter[] dbParameters)> DataAdapterFillAsync(this DatabaseFacade databaseFacade, string sql, object model, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
    {
        // 初始化数据库连接对象和数据库命令对象
        var (_, dbCommand, dbParameters) = await databaseFacade.PrepareDbCommandAsync(sql, model, commandType, cancellationToken);

        // 读取数据
        using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

        // 填充到 DataSet
        var dataSet = dbDataReader.ToDataSet();

        // 释放命令对象
        dbCommand.Parameters?.Clear();
        await dbCommand.DisposeAsync();

        return (dataSet, dbParameters);
    }
}