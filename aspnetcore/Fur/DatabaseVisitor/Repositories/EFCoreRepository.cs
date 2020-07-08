using Autofac;
using Autofac.Extensions.DependencyInjection;
using Fur.DatabaseVisitor.Entities;
using Fur.DependencyInjection.Lifetimes;
using System;

namespace Fur.DatabaseVisitor.Repositories
{
    public partial class EFCoreRepository : IRepository, IScopedLifetime
    {
        private readonly IServiceProvider _serviceProvider;

        public EFCoreRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRepositoryOfT<TEntity> GetRepository<TEntity>(bool newScope = false) where TEntity : class, IDbEntity, new()
        {
            if (newScope)
            {
                return _serviceProvider.GetAutofacRoot().BeginLifetimeScope().Resolve<IRepositoryOfT<TEntity>>();
            }
            return _serviceProvider.GetAutofacRoot().Resolve<IRepositoryOfT<TEntity>>();
        }
    }
}