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

using Fur.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 操作仓储实现
    /// </summary>
    [NonBeScan]
    public partial class SqlRepository : SqlRepository<DbContextLocator>, ISqlRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextResolve">数据库上下文解析器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        public SqlRepository(
            Func<Type, DbContext> dbContextResolve
            , IDbContextPool dbContextPool) : base(dbContextResolve, dbContextPool)
        {
        }
    }

    /// <summary>
    /// Sql 操作仓储实现
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    [NonBeScan]
    public partial class SqlRepository<TDbContextLocator> : ISqlRepository<TDbContextLocator>
        where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextResolve">数据库上下文解析器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        public SqlRepository(
            Func<Type, DbContext> dbContextResolve
            , IDbContextPool dbContextPool)
        {
            // 解析数据库上下文
            var dbContext = dbContextResolve(typeof(TDbContextLocator));

            // 保存当前数据库上下文到池中
            dbContextPool.AddToPool(dbContext);

            // 初始化数据库相关数据
            Database = dbContext.Database;
        }

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public DatabaseFacade Database { get; }
    }
}