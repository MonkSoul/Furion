// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（https://gitee.com/monksoul/Fur/blob/alpha/LICENSE）

using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// ADONET 拓展类
    /// </summary>
    public static class SqlADONETExtensions
    {
        /// <summary>
        /// 执行 Sql 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="behavior">行为</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteReader(this DatabaseFacade databaseFacade, string sql, object[] parameters = null, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default)
        {
            // 初始化数据库连接对象和数据库命令对象
            var (dbConnection, dbCommand) = databaseFacade.PrepareDbCommand(sql, commandType, parameters);

            // 读取数据
            using var dbDataReader = dbCommand.ExecuteReader(behavior);

            // 填充到 DataTable
            using var dataTable = new DataTable();
            dataTable.Load(dbDataReader);

            // 关闭连接
            dbDataReader.Close();
            dbConnection.Close();

            // 清空命令参数
            dbCommand.Parameters.Clear();

            return dataTable;
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
        public static async Task<DataTable> ExecuteReaderAsync(this DatabaseFacade databaseFacade, string sql, object[] parameters = null, CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            // 初始化数据库连接对象和数据库命令对象
            var (dbConnection, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, commandType, parameters);

            // 读取数据
            using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

            // 填充到 DataTable
            using var dataTable = new DataTable();
            dataTable.Load(dbDataReader);

            // 关闭连接
            await dbDataReader.CloseAsync();
            await dbConnection.CloseAsync();

            // 清空命令参数
            dbCommand.Parameters.Clear();

            return dataTable;
        }

        /// <summary>
        /// 执行 Sql 语句返回受影响行数
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery(this DatabaseFacade databaseFacade, string sql, object[] parameters = null, CommandType commandType = CommandType.Text)
        {
            // 初始化数据库连接对象和数据库命令对象
            var (dbConnection, dbCommand) = databaseFacade.PrepareDbCommand(sql, commandType, parameters);

            // 执行返回受影响行数
            var rowEffects = dbCommand.ExecuteNonQuery();

            // 关闭连接
            dbConnection.Close();

            // 清空命令参数
            dbCommand.Parameters.Clear();

            return rowEffects;
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
        public static async Task<int> ExecuteNonQueryAsync(this DatabaseFacade databaseFacade, string sql, object[] parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            // 初始化数据库连接对象和数据库命令对象
            var (dbConnection, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, commandType, parameters);

            // 执行返回受影响行数
            var rowEffects = await dbCommand.ExecuteNonQueryAsync(cancellationToken);

            // 关闭连接
            await dbConnection.CloseAsync();

            // 清空命令参数
            dbCommand.Parameters.Clear();

            return rowEffects;
        }

        /// <summary>
        /// 执行 Sql 返回单行单列的值
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>单行单列的值</returns>
        public static object ExecuteScalar(this DatabaseFacade databaseFacade, string sql, object[] parameters = null, CommandType commandType = CommandType.Text)
        {
            // 初始化数据库连接对象和数据库命令对象
            var (dbConnection, dbCommand) = databaseFacade.PrepareDbCommand(sql, commandType, parameters);

            // 执行返回单行单列的值
            var result = dbCommand.ExecuteScalar();

            // 关闭连接
            dbConnection.Close();

            // 清空命令参数
            dbCommand.Parameters.Clear();

            return result;
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
        public static async Task<object> ExecuteScalarAsync(this DatabaseFacade databaseFacade, string sql, object[] parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            // 初始化数据库连接对象和数据库命令对象
            var (dbConnection, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, commandType, parameters);

            // 执行返回单行单列的值
            var result = await dbCommand.ExecuteScalarAsync(cancellationToken);

            // 关闭连接
            await dbConnection.CloseAsync();

            // 清空命令参数
            dbCommand.Parameters.Clear();

            return result;
        }

        /// <summary>
        /// 执行 Sql 返回 DataSet
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>DataSet</returns>
        public static DataSet DataAdapterFill(this DatabaseFacade databaseFacade, string sql, object[] parameters = null, CommandType commandType = CommandType.Text)
        {
            // 初始化数据库连接对象、数据库命令对象和数据库适配器对象
            var (dbConnection, dbCommand, dbDataAdapter) = databaseFacade.PrepareDbDbDataAdapter(sql, commandType, parameters);

            // 填充DataSet
            using var dataSet = new DataSet();
            dbDataAdapter.Fill(dataSet);

            // 关闭连接
            dbConnection.Close();

            // 清空命令参数
            dbCommand.Parameters.Clear();

            return dataSet;
        }

        /// <summary>
        /// 执行 Sql 返回 DataSet
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        public static async Task<DataSet> DataAdapterFillAsync(this DatabaseFacade databaseFacade, string sql, object[] parameters = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            // 初始化数据库连接对象、数据库命令对象和数据库适配器对象
            var (dbConnection, dbCommand, dbDataAdapter) = await databaseFacade.PrepareDbDbDataAdapterAsync(sql, commandType, parameters);

            // 填充DataSet
            using var dataSet = new DataSet();
            dbDataAdapter.Fill(dataSet);

            // 关闭连接
            await dbConnection.CloseAsync();

            // 清空命令参数
            dbCommand.Parameters.Clear();

            return dataSet;
        }
    }
}