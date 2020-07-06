using Castle.DynamicProxy;
using Fur.DatabaseVisitor.TenantSaaS;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.DatabaseVisitor.Tangent
{
    public class TangentDbContext : ITangentDbContext, IScopedLifetime
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContext _dbContext;
        private readonly ITenantProvider _tenantProvider;

        public TangentDbContext(
            IServiceProvider serviceProvider
            , DbContext dbContext
            , ITenantProvider tenantProvider)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
            _tenantProvider = tenantProvider;
        }

        public TTangent For<TTangent>() where TTangent : class, ITangentQueryDependency
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<TTangent>(new TangentInterceptor(new TangentAsyncInterceptor(_serviceProvider, _dbContext, _tenantProvider)));
        }
    }
}