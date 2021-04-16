using Furion.DependencyInjection;
using System;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 实体执行部件
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [SkipScan]
    public sealed partial class EntityExecutePart<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 实体
        /// </summary>
        public TEntity Entity { get; private set; }

        /// <summary>
        /// 数据库上下文定位器
        /// </summary>
        public Type DbContextLocator { get; private set; } = typeof(MasterDbContextLocator);

        /// <summary>
        /// 数据库上下文执行作用域
        /// </summary>
        public IServiceProvider ContextScoped { get; private set; }
    }
}