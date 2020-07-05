using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Dependencies;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DependencyInjection.Lifetimes;
using System;

namespace Fur.DatabaseVisitor.Repositories
{
    public class MultipleEFCoreRepository : IMultipleRepository, IScopedLifetime
    {
        private readonly IServiceProvider _serviceProvider;

        public MultipleEFCoreRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMultipleRepositoryOfT<TEntity, TDbContextIdentifier> GetMultipleRepository<TEntity, TDbContextIdentifier>(bool newScope = false)
            where TEntity : class, IEntity, new()
            where TDbContextIdentifier : IDbContextIdentifier
        {
            if (newScope)
            {
                return _serviceProvider.GetAutofacRoot().BeginLifetimeScope().Resolve<IMultipleRepositoryOfT<TEntity, TDbContextIdentifier>>();
            }
            return _serviceProvider.GetAutofacRoot().Resolve<IMultipleRepositoryOfT<TEntity, TDbContextIdentifier>>();
        }
    }
}