using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Entities;
using Fur.DependencyInjection.Lifetimes;
using System;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 非泛型仓储实现类
    /// </summary>
    public partial class EFCoreRepository : IRepository, IScopedLifetime
    {
        /// <summary>
        /// 注入服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        #region 构造函数 + public EFCoreRepository(IServiceProvider serviceProvider)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public EFCoreRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region 获取泛型仓储接口 + public IRepositoryOfT<TEntity> GetRepository<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new()
        /// <summary>
        /// 获取泛型仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IRepositoryOfT{TEntity}"/></returns>
        public IRepositoryOfT<TEntity> GetRepository<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new()
        {
            if (newScope)
            {
                return _serviceProvider.GetAutofacRoot().BeginLifetimeScope().Resolve<IRepositoryOfT<TEntity>>();
            }
            return _serviceProvider.GetAutofacRoot().Resolve<IRepositoryOfT<TEntity>>();
        }
        #endregion
    }
}