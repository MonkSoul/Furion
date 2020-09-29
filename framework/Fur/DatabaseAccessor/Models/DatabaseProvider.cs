// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Fur.FriendlyException;
using System;
using System.Data;
using System.Linq;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库提供器选项
    /// </summary>
    [SkipScan]
    internal static class DatabaseProvider
    {
        /// <summary>
        /// SqlServer 提供器程序集
        /// </summary>
        internal const string SqlServer = "Microsoft.EntityFrameworkCore.SqlServer";

        /// <summary>
        /// Sqlite 提供器程序集
        /// </summary>
        internal const string Sqlite = "Microsoft.EntityFrameworkCore.Sqlite";

        /// <summary>
        /// Cosmos 提供器程序集
        /// </summary>
        internal const string Cosmos = "Microsoft.EntityFrameworkCore.Cosmos";

        /// <summary>
        /// 内存数据库 提供器程序集
        /// </summary>
        internal const string InMemory = "Microsoft.EntityFrameworkCore.InMemory";

        /// <summary>
        /// MySql 提供器程序集
        /// </summary>
        internal const string MySql = "Pomelo.EntityFrameworkCore.MySql";

        /// <summary>
        /// PostgreSQL 提供器程序集
        /// </summary>
        internal const string PostgreSQL = "Npgsql.EntityFrameworkCore.PostgreSQL";

        /// <summary>
        /// Oracle 提供器程序集
        /// </summary>
        internal const string Oracle = "Citms.EntityFrameworkCore.Oracle";

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
        static DatabaseProvider()
        {
            NotSupportStoredProcedureDatabases = new[]
            {
                Sqlite,
                InMemory
            };

            NotSupportFunctionDatabases = new[]
            {
                Sqlite,
                InMemory
            };

            NotSupportTableFunctionDatabases = new[]
            {
                Sqlite,
                InMemory,
                MySql
            };

            NotSupportTransactionScopeDatabase = new[]
            {
                Sqlite,
                InMemory,
            };
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
                throw Oops.Oh(NotSupportException, typeof(NotSupportedException), "stored procedure");
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
                throw Oops.Oh(NotSupportException, typeof(NotSupportedException), "function");
            }

            if (dbFunctionType == DbFunctionType.Table && NotSupportTableFunctionDatabases.Contains(providerName))
            {
                throw Oops.Oh(NotSupportException, typeof(NotSupportedException), "table function");
            }
        }
    }
}