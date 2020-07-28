using Autofac;
using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Contexts.Pools;
using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseAccessor.Repositories.Multiple
{
    /// <summary>
    /// 泛型多上下文仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContextIdentifier"></typeparam>
    public partial class EFCoreRepositoryOfT<TEntity, TDbContextIdentifier> : EFCoreRepositoryOfT<TEntity>, IRepositoryOfT<TEntity, TDbContextIdentifier>
        where TEntity : class, IDbEntityBase, new()
        where TDbContextIdentifier : IDbContextIdentifier
    {
        #region 构造函数 + public MultipleDbContextEFCoreRepositoryOfT(ILifetimeScope lifetimeScope,IDbContextPool dbContextPool)

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        public EFCoreRepositoryOfT(
            ILifetimeScope lifetimeScope
            , IDbContextPool dbContextPool)
            : base(lifetimeScope.ResolveNamed<DbContext>(typeof(TDbContextIdentifier).Name), lifetimeScope, dbContextPool)
        {
        }

        #endregion
    }
}