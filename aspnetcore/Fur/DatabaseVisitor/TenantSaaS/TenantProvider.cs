using Fur.DependencyInjection.Lifetimes;
using Microsoft.AspNetCore.Http;

namespace Fur.DatabaseVisitor.TenantSaaS
{
    public class TenantProvider : ITenantProvider, IScopedLifetime
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FurTenantDbContext _tenantDbContext;

        public TenantProvider(IHttpContextAccessor httpContextAccessor, FurTenantDbContext tenantDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantDbContext = tenantDbContext;
        }

        public int GetTenantId()
        {
            var host = _httpContextAccessor.HttpContext.Request.Host.Value;
            return _tenantDbContext.GetTenantId(host);
        }
    }
}