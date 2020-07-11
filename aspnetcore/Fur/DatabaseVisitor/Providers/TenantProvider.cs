using Fur.DatabaseVisitor.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Fur.DatabaseVisitor.Providers
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FurTenantDbContext _tenantDbContext;
        private readonly IMemoryCache _memoryCache;

        public TenantProvider(IHttpContextAccessor httpContextAccessor
            , FurTenantDbContext tenantDbContext
            , IMemoryCache memoryCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantDbContext = tenantDbContext;
            _memoryCache = memoryCache;
        }

        public int GetTenantId()
        {
            if (_httpContextAccessor?.HttpContext == null) return default;

            var host = _httpContextAccessor.HttpContext.Request.Host.Value;

            var isExistsKey = _memoryCache.TryGetValue(host, out object value);
            if (isExistsKey) return Convert.ToInt32(value);
            else
            {
                var tenantId = _tenantDbContext.GetTenantId(host);
                _memoryCache.Set(host, tenantId);
                return tenantId;
            }
        }
    }
}