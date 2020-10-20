// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库提供器选项
    /// </summary>
    [SkipScan]
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
        /// PostgreSQL 提供器程序集
        /// </summary>
        internal const string Npgsql = "Npgsql.EntityFrameworkCore.PostgreSQL";

        /// <summary>
        /// Oracle 提供器程序集
        /// </summary>
        public const string Oracle = "Citms.EntityFrameworkCore.Oracle";

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
        /// 不支持环境事务的数据库
        /// </summary>
        internal static readonly string[] NotSupportTransactionScopeDatabase;

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
                Firebird,
                Dm
            };

            NotSupportTransactionScopeDatabase = new[]
            {
                Sqlite,
                InMemoryDatabase
            };
        }

        /// <summary>
        /// 获取数据库上下文连接字符串
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string GetDbContextConnectionString<TDbContext>(string connectionString = default)
            where TDbContext : DbContext
        {
            if (!string.IsNullOrEmpty(connectionString)) return connectionString;

            // 如果没有配置数据库连接字符串，那么查找特性
            var dbContextType = typeof(TDbContext);
            if (!dbContextType.IsDefined(typeof(AppDbContextAttribute), true)) return default;

            // 获取配置特性
            var dbContextAttribute = dbContextType.GetCustomAttribute<AppDbContextAttribute>(true);
            var connStr = dbContextAttribute.ConnectionString;

            if (string.IsNullOrEmpty(connStr)) return default;
            // 如果包含 = 符号，那么认为是连接字符串
            if (connStr.Contains("=")) return connStr;
            else
            {
                var configuration = App.Configuration;

                // 如果包含 : 符号，那么认为是一个 Key 路径
                if (connStr.Contains(":")) return configuration[connStr];
                else
                {
                    // 首先查找 DbConnectionString 键，如果没有找到，则当成 Key 去查找
                    var connStrValue = configuration.GetConnectionString(connStr);
                    return !string.IsNullOrEmpty(connStrValue) ? connStrValue : configuration[connStr];
                }
            }
        }

        /// <summary>
        /// 不支持操作类型
        /// </summary>
        private const string NotSupportException = "The database provider does not support {0} operations";

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
}