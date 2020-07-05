using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Dependencies;
using Fur.DatabaseVisitor.Identifiers;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.DatabaseVisitor.Repositories
{
    public class MultipleEFCoreRepositoryOfT<TEntity, TDbContextIdentifier> : EFCoreRepositoryOfT<TEntity>, IMultipleRepositoryOfT<TEntity, TDbContextIdentifier>
        where TEntity : class, IEntity, new()
        where TDbContextIdentifier : IDbContextIdentifier
    {
        public MultipleEFCoreRepositoryOfT(
            IServiceProvider serviceProvider)
            : base(serviceProvider.GetAutofacRoot().ResolveNamed<DbContext>(typeof(TDbContextIdentifier).Name), serviceProvider)
        {
        }
    }
}
