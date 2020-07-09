using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Contexts;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.TenantSaaS;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.DatabaseVisitor.Repositories.Multiples
{
    /// <summary>
    /// 泛型多上下文仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContextIdentifier"></typeparam>
    public partial class MultipleDbContextEFCoreRepositoryOfT<TEntity, TDbContextIdentifier> : EFCoreRepositoryOfT<TEntity>, IMultipleDbContextRepositoryOfT<TEntity, TDbContextIdentifier>
        where TEntity : class, IDbEntity, new()
        where TDbContextIdentifier : IDbContextIdentifier
    {
        #region 构造函数 + public MultipleDbContextEFCoreRepositoryOfT(IServiceProvider serviceProvider, ITenantProvider tenantProvider, IDbContextPool dbContextPool)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="tenantProvider">租户提供器</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        public MultipleDbContextEFCoreRepositoryOfT(
            IServiceProvider serviceProvider
            , ITenantProvider tenantProvider
            , IDbContextPool dbContextPool)
            : base(serviceProvider.GetAutofacRoot().ResolveNamed<DbContext>(typeof(TDbContextIdentifier).Name), serviceProvider, tenantProvider, dbContextPool)
        {
        }
        #endregion
    }
}