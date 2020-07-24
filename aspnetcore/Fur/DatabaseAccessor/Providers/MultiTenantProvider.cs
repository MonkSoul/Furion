using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.Contexts.Pool;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Fur.DatabaseAccessor.Providers
{
    public class MultiTenantProvider : IMultiTenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FurMultiTenantDbContext _tenantDbContext;
        private readonly IMemoryCache _memoryCache;
        private readonly IDbContextPool _dbContextPool;

        public MultiTenantProvider(IHttpContextAccessor httpContextAccessor
            , FurMultiTenantDbContext tenantDbContext
            , IMemoryCache memoryCache
            , IDbContextPool dbContextPool)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantDbContext = tenantDbContext;
            _memoryCache = memoryCache;
            _dbContextPool = dbContextPool;

            // 自定义提供器必须实现
            _dbContextPool.SaveDbContext(_tenantDbContext);
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