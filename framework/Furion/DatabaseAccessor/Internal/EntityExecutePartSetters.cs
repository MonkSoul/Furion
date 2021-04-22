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