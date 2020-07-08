using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.DbContextPool;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.TenantSaaS;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.DatabaseVisitor.Repositories
{
    public class MultipleEFCoreRepositoryOfT<TEntity, TDbContextIdentifier> : EFCoreRepositoryOfT<TEntity>, IMultipleRepositoryOfT<TEntity, TDbContextIdentifier>
        where TEntity : class, IDbEntity, new()
        where TDbContextIdentifier : IDbContextIdentifier
    {
        public MultipleEFCoreRepositoryOfT(
            IServiceProvider serviceProvider
            , ITenantProvider tenantProvider
             , IDbContextPool dbContextPool)
            : base(serviceProvider.GetAutofacRoot().ResolveNamed<DbContext>(typeof(TDbContextIdentifier).Name), serviceProvider, tenantProvider, dbContextPool)
        {
        }
    }
}