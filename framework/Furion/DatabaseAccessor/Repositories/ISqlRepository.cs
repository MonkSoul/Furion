// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 操作仓储接口
    /// </summary>
    public interface ISqlRepository : ISqlRepository<MasterDbContextLocator>
        , ISqlExecutableRepository
        , ISqlReaderRepository
    {
    }

    /// <summary>
    /// Sql 操作仓储接口
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public interface ISqlRepository<TDbContextLocator> : IPrivateSqlRepository
        , ISqlExecutableRepository<TDbContextLocator>
        , ISqlReaderRepository<TDbContextLocator>
       where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 私有 Sql 仓储
    /// </summary>
    public interface IPrivateSqlRepository : IPrivateSqlExecutableRepository
        , IPrivateSqlReaderRepository
        , IPrivateRootRepository
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        dynamic DynamicContext { get; }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>仓储</returns>
        ISqlRepository<TChangeDbContextLocator> Change<TChangeDbContextLocator>()
            where TChangeDbContextLocator : class, IDbContextLocator;

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService GetService<TService>()
            where TService : class;

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService GetRequiredService<TService>()
            where TService : class;

        /// <summary>
        /// 将仓储约束为特定仓储
        /// </summary>
        /// <typeparam name="TRestrainRepository">特定仓储</typeparam>
        /// <returns>TRestrainRepository</returns>
        TRestrainRepository Constraint<TRestrainRepository>()
            where TRestrainRepository : class, IPrivateRootRepository;

        /// <summary>
        /// 确保工作单元（事务）可用
        /// </summary>
        void EnsureTransaction();
    }
}