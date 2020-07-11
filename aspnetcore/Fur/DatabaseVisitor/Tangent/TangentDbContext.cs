using Autofac;
using Castle.DynamicProxy;
using Fur.DatabaseVisitor.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseVisitor.Tangent
{
    public class TangentDbContext : ITangentDbContext/*, IScopedLifetime*/
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly DbContext _dbContext;
        //private readonly ITenantProvider _tenantProvider;

        public TangentDbContext(
            ILifetimeScope lifetimeScope
            , DbContext dbContext
            //, ITenantProvider tenantProvider
            , IDbContextPool dbContextPool)
        {
            _lifetimeScope = lifetimeScope;
            _dbContext = dbContext;
            dbContextPool.SaveDbContext(dbContext);

            //_tenantProvider = tenantProvider;
        }

        public TTangent For<TTangent>() where TTangent : class, ITangentQueryDependency
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<TTangent>(new TangentInterceptor(new TangentAsyncInterceptor(_lifetimeScope, _dbContext/*, _tenantProvider*/)));
        }
    }
}