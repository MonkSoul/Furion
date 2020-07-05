using Castle.DynamicProxy;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;

namespace Fur.DatabaseVisitor.Tangent
{
    public class TangentDbContext : ITangentDbContext, IScopedLifetime
    {
        private readonly DbContext _dbContext;
        public TangentDbContext(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TTangent For<TTangent>() where TTangent : class, ITangentQueryDependency
        {
            return new ProxyGenerator().CreateInterfaceProxyWithoutTarget<TTangent>(new TangentInterceptor(new TangentAsyncInterceptor(_dbContext)));
        }
    }
}
