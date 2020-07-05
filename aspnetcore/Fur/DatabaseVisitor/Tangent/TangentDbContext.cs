using Castle.DynamicProxy;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fur.DatabaseVisitor.Tangent
{
    public class TangentDbContext : ITangentDbContext, IScopedLifetime
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContext _dbContext;
        public TangentDbContext(
            IServiceProvider serviceProvider
            , DbContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        public TTangent For<TTangent>() where TTangent : class, ITangentQueryDependency
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<TTangent>(new TangentInterceptor(new TangentAsyncInterceptor(_serviceProvider, _dbContext)));
        }
    }
}
