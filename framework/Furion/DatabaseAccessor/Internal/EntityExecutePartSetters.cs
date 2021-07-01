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

using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 实体执行部件
    /// </summary>
    public sealed partial class EntityExecutePart<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EntityExecutePart<TEntity> SetEntity(TEntity entity)
        {
            Entity = entity;
            return this;
        }

        /// <summary>
        /// 设置数据库执行作用域
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public EntityExecutePart<TEntity> SetContextScoped(IServiceProvider serviceProvider)
        {
            ContextScoped = serviceProvider;
            return this;
        }

        /// <summary>
        /// 设置数据库上下文定位器
        /// </summary>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <returns></returns>
        public EntityExecutePart<TEntity> Change<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            DbContextLocator = typeof(TDbContextLocator) ?? typeof(MasterDbContextLocator);
            return this;
        }

        /// <summary>
        /// 设置数据库上下文定位器
        /// </summary>
        /// <returns></returns>
        public EntityExecutePart<TEntity> Change(Type dbContextLocator)
        {
            DbContextLocator = dbContextLocator ?? typeof(MasterDbContextLocator);
            return this;
        }
    }
}