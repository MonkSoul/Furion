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
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 数据库公开类
    /// </summary>
    [SkipScan]
    public static class Db
    {
        /// <summary>
        /// 不支持解析服务错误提示
        /// </summary>
        private const string NotSupportedResolveMessage = "Reading {0} instances on non HTTP requests is not supported.";

        /// <summary>
        /// 获取非泛型仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        public static IRepository GetRepository()
        {
            return App.GetRequestService<IRepository>()
                ?? throw new NotSupportedException(string.Format(NotSupportedResolveMessage, nameof(IRepository)));
        }

        /// <summary>
        /// 获取实体仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>IRepository<TEntity></returns>
        public static IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IPrivateEntity, new()
        {
            return App.GetRequestService<IRepository<TEntity>>()
                ?? throw new NotSupportedException(string.Format(NotSupportedResolveMessage, nameof(IRepository<TEntity>)));
        }

        /// <summary>
        /// 获取实体仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>IRepository<TEntity, TDbContextLocator></returns>
        public static IRepository<TEntity, TDbContextLocator> GetRepository<TEntity, TDbContextLocator>()
            where TEntity : class, IPrivateEntity, new()
            where TDbContextLocator : class, IDbContextLocator
        {
            return App.GetRequestService<IRepository<TEntity, TDbContextLocator>>()
                ?? throw new NotSupportedException(string.Format(NotSupportedResolveMessage, nameof(IRepository<TEntity, TDbContextLocator>)));
        }

        /// <summary>
        /// 获取Sql仓储
        /// </summary>
        /// <returns>ISqlRepository</returns>
        public static ISqlRepository GetSqlRepository()
        {
            return App.GetRequestService<ISqlRepository>()
                ?? throw new NotSupportedException(string.Format(NotSupportedResolveMessage, nameof(ISqlRepository)));
        }

        /// <summary>
        /// 获取Sql仓储
        /// </summary>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>ISqlRepository<TDbContextLocator></returns>
        public static ISqlRepository<TDbContextLocator> GetSqlRepository<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            return App.GetRequestService<ISqlRepository<TDbContextLocator>>()
                ?? throw new NotSupportedException(string.Format(NotSupportedResolveMessage, nameof(ISqlRepository<TDbContextLocator>)));
        }

        /// <summary>
        /// 获取Sql代理
        /// </summary>
        /// <returns>ISqlRepository</returns>
        public static TSqlDispatchProxy GetSqlDispatchProxy<TSqlDispatchProxy>()
            where TSqlDispatchProxy : class, ISqlDispatchProxy
        {
            return App.GetRequestService<TSqlDispatchProxy>()
                ?? throw new NotSupportedException(string.Format(NotSupportedResolveMessage, nameof(ISqlDispatchProxy)));
        }

        /// <summary>
        /// 获取瞬时数据库上下文
        /// </summary>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns></returns>
        public static DbContext GetDbContext<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            var dbContextResolve = App.GetService<Func<Type, ITransient, DbContext>>();
            return dbContextResolve(typeof(TDbContextLocator), default);
        }

        /// <summary>
        /// 获取作用域数据库上下文
        /// </summary>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns></returns>
        public static DbContext GetRequestDbContext<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            var dbContextResolve = App.GetRequestService<Func<Type, IScoped, DbContext>>();
            return dbContextResolve(typeof(TDbContextLocator), default);
        }
    }
}