using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DependencyInjection.Lifetimes;
using System;

namespace Fur.DatabaseVisitor.Repositories.Multiples
{
    /// <summary>
    /// 非泛型多上下文的仓储实现类
    /// </summary>
    public partial class MultipleDbContextEFCoreRepository : IMultipleDbContextRepository, IScopedLifetime
    {
        /// <summary>
        /// 注入服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        #region 构造函数 + public MultipleDbContextEFCoreRepository(IServiceProvider serviceProvider)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public MultipleDbContextEFCoreRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #endregion

        #region 获取泛型多上下文仓储接口 +  public IMultipleDbContextRepositoryOfT<TEntity, TDbContextIdentifier> GetMultipleDbContextRepository<TEntity, TDbContextIdentifier>(bool newScope = false) where TEntity : class, IDbEntity, new() where TDbContextIdentifier : IDbContextIdentifier
        /// <summary>
        /// 获取泛型多上下文仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TDbContextIdentifier">数据上下文标识类。参见：<see cref="FurDbContextIdentifier"/></typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IMultipleDbContextRepositoryOfT{TEntity, TDbContextIdentifier}"/></returns>
        public IMultipleDbContextRepositoryOfT<TEntity, TDbContextIdentifier> GetMultipleDbContextRepository<TEntity, TDbContextIdentifier>(bool newScope = false)
            where TEntity : class, IDbEntity, new()
            where TDbContextIdentifier : IDbContextIdentifier
        {
            if (newScope)
            {
                return _serviceProvider.GetAutofacRoot().BeginLifetimeScope().Resolve<IMultipleDbContextRepositoryOfT<TEntity, TDbContextIdentifier>>();
            }
            return _serviceProvider.GetAutofacRoot().Resolve<IMultipleDbContextRepositoryOfT<TEntity, TDbContextIdentifier>>();
        }
        #endregion
    }
}