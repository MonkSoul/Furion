using Autofac;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 非泛型仓储实现类
    /// </summary>
    public partial class EFCoreRepository : IRepository
    {
        /// <summary>
        /// Autofac生命周期对象
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        #region 构造函数 + public EFCoreRepository(ILifetimeScope lifetimeScope)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ILifetimeScope">Autofac生命周期实例</param>
        public EFCoreRepository(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        #endregion 构造函数 + public EFCoreRepository(ILifetimeScope lifetimeScope)

        #region 获取泛型仓储接口 + public IRepositoryOfT<TEntity> Set<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new()

        /// <summary>
        /// 获取泛型仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IRepositoryOfT{TEntity}"/></returns>
        public IRepositoryOfT<TEntity> Set<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new()
        {
            if (newScope)
            {
                return _lifetimeScope.BeginLifetimeScope().Resolve<IRepositoryOfT<TEntity>>();
            }
            return _lifetimeScope.Resolve<IRepositoryOfT<TEntity>>();
        }

        #endregion 获取泛型仓储接口 + public IRepositoryOfT<TEntity> Set<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new()
    }
}