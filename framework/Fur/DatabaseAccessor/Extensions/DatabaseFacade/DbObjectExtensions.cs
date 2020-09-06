// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// DatabaseFacade 拓展类
    /// </summary>
    public static class DbObjectExtensions
    {
        /// <summary>
        /// MiniProfiler 组件状态
        /// </summary>
        private static readonly bool InjectMiniProfiler;

        /// <summary>
        /// 构造函数
        /// </summary>
        static DbObjectExtensions()
        {
            InjectMiniProfiler = App.Settings.InjectMiniProfiler.Value;
        }

        /// <summary>
        /// 初始化数据库命令对象
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>数据库连接对象及数据库命令对象</returns>
        public static (DbConnection dbConnection, DbCommand dbCommand) PrepareDbCommand(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            // 创建数据库连接对象及数据库命令对象
            var (dbConnection, dbCommand) = databaseFacade.CreateDbCommand(sql, commandType, parameters);

            // 判断连接字符串是否关闭，如果是，则开启
            if (dbConnection.State == ConnectionState.Closed) dbConnection.Open();

            // 返回
            return (dbConnection, dbCommand);
        }

        /// <summary>
        /// 初始化数据库命令对象
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>数据库连接对象及数据库命令对象</returns>
        public static async Task<(DbConnection dbConnection, DbCommand dbCommand)> PrepareDbCommandAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            // 创建数据库连接对象及数据库命令对象
            var (dbConnection, dbCommand) = databaseFacade.CreateDbCommand(sql, commandType, parameters);

            // 判断连接字符串是否关闭，如果是，则开启
            if (dbConnection.State == ConnectionState.Closed) await dbConnection.OpenAsync();

            // 返回
            return (dbConnection, dbCommand);
        }

        /// <summary>
        /// 初始化数据库适配器对象
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>数据库连接对象、数据库命令对象和数据库适配器对象</returns>
        public static (DbConnection dbConnection, DbCommand dbCommand, DbDataAdapter dbDataAdapter) PrepareDbDbDataAdapter(this DatabaseFacade databaseFacade, string sql, CommandType commandType, params object[] parameters)
        {
            // 创建数据库连接对象、数据库命令对象和数据库适配器对象
            var (dbConnection, dbCommand, dbDataAdapter) = databaseFacade.CreateDbDataAdapter(sql, commandType, parameters);

            // 判断连接字符串是否关闭，如果是，则开启
            if (dbConnection.State == ConnectionState.Closed) dbConnection.Open();

            // 返回
            return (dbConnection, dbCommand, dbDataAdapter);
        }

        /// <summary>
        /// 初始化数据库适配器对象
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>数据库连接对象、数据库命令对象和数据库适配器对象</returns>
        public static async Task<(DbConnection dbConnection, DbCommand dbCommand, DbDataAdapter dbDataAdapter)> PrepareDbDbDataAdapterAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType, params object[] parameters)
        {
            // 创建数据库连接对象、数据库命令对象和数据库适配器对象
            var (dbConnection, dbCommand, dbDataAdapter) = databaseFacade.CreateDbDataAdapter(sql, commandType, parameters);

            // 判断连接字符串是否关闭，如果是，则开启
            if (dbConnection.State == ConnectionState.Closed) await dbConnection.OpenAsync();

            // 返回
            return (dbConnection, dbCommand, dbDataAdapter);
        }

        /// <summary>
        /// 创建数据库命令对象
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>数据库连接对象及数据库命令对象</returns>
        private static (DbConnection dbConnection, DbCommand dbCommand) CreateDbCommand(this DatabaseFacade databaseFacade, string sql, CommandType commandType, params object[] parameters)
        {
            // 判断是否启用 MiniProfiler 组件，如果有，则包装链接
            var dbConnection = InjectMiniProfiler ? new ProfiledDbConnection(databaseFacade.GetDbConnection(), MiniProfiler.Current) : databaseFacade.GetDbConnection();

            // 创建数据库命令对象
            var dbCommand = dbConnection.CreateCommand();
            // 设置基本参数
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = sql;
            SetDbCommandParameters(ref dbCommand, parameters);

            // 返回
            return (dbConnection, dbCommand);
        }

        /// <summary>
        /// 创建数据库适配器
        /// </summary>
        /// <param name="databaseFacade">ADO.NET 数据库对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>数据库连接对象、数据库命令对象和数据库适配器对象</returns>
        private static (DbConnection dbConnection, DbCommand dbCommand, DbDataAdapter dbDataAdapter) CreateDbDataAdapter(this DatabaseFacade databaseFacade, string sql, CommandType commandType, params object[] parameters)
        {
            // 获取数据库连接字符串
            var dbConnection = databaseFacade.GetDbConnection();

            // 解析数据库提供器
            var dbProviderFactory = DbProviderFactories.GetFactory(dbConnection);

            // 判断是否启用 MiniProfiler 组件，如果有，则包装链接和数据库提供器工厂
            var profiledDbProviderFactory = InjectMiniProfiler ? new ProfiledDbProviderFactory(dbProviderFactory, true) : dbProviderFactory;

            // 创建数据库连接对象及数据库命令对象
            var (_dbConnection, dbCommand) = databaseFacade.CreateDbCommand(sql, commandType, parameters);
            dbConnection = _dbConnection;

            // 创建数据适配器并设置查询命令对象
            var dbDataAdapter = profiledDbProviderFactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = dbCommand;

            // 返回
            return (dbConnection, dbCommand, dbDataAdapter);
        }

        /// <summary>
        /// 设置数据库命令对象参数
        /// </summary>
        /// <param name="dbCommand">数据库命令对象</param>
        /// <param name="parameters">命令参数</param>
        private static void SetDbCommandParameters(ref DbCommand dbCommand, params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0) return;

            // 添加 @ 前缀
            foreach (SqlParameter parameter in parameters)
            {
                if (!parameter.ParameterName.Contains("@"))
                {
                    parameter.ParameterName = $"@{parameter.ParameterName}";
                }
                dbCommand.Parameters.Add(parameter);
            }
        }
    }
}