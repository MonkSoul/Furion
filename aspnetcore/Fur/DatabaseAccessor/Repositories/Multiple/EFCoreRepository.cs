using Autofac;
using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Contexts.Pools;
using Fur.DatabaseAccessor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseAccessor.Repositories.Multiple
{
    /// <summary>
    /// 泛型多上下文仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContextLocator"></typeparam>
    public partial class EFCoreRepository<TEntity, TDbContextLocator> : Repositories.EFCoreRepository<TEntity>, IRepository<TEntity, TDbContextLocator>
        where TEntity : class, IDbEntityBase, new()
        where TDbContextLocator : IDbContextLocator
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        public EFCoreRepository(
            ILifetimeScope lifetimeScope
            , IDbContextPool dbContextPool)
            : base(lifetimeScope.ResolveNamed<DbContext>(typeof(TDbContextLocator).Name), lifetimeScope, dbContextPool)
        {
        }
    }

    /// <summary>
    /// 多上下文仓储实例
    /// <para>也就是可以支持多个 <see cref="Microsoft.EntityFrameworkCore.DbContext"/> 的仓储</para>
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial class EFCoreRepository<TDbContextLocator> : IRepository<TDbContextLocator>
        where TDbContextLocator : IDbContextLocator
    {
        /// <summary>
        /// Autofac生命周期对象
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">Autofac生命周期对象</param>
        public EFCoreRepository(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// 获取泛型多上下文仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IRepository{TEntity, TDbContextLocator}"/></returns>
        public IRepository<TEntity, TDbContextLocator> Set<TEntity>(bool newScope = false)
            where TEntity : class, IDbEntityBase, new()
        {
            if (newScope)
            {
                return _lifetimeScope.BeginLifetimeScope().Resolve<IRepository<TEntity, TDbContextLocator>>();
            }
            return _lifetimeScope.Resolve<IRepository<TEntity, TDbContextLocator>>();
        }
    }
}