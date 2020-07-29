using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Fur.EntityFramework.Core.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Fur.EntityFramework.Core.MultipleTenantProviders
{
    public class FurMultipleTenantOnDatabaseProvider : IMultipleTenantOnDatabaseProvider
    {
        private readonly FurMultipleTenantDbContext _multipleTenantDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;

        public FurMultipleTenantOnDatabaseProvider(FurMultipleTenantDbContext multipleTenantDbContext
            , IHttpContextAccessor httpContextAccessor
            , IMemoryCache memoryCache)
        {
            _multipleTenantDbContext = multipleTenantDbContext;
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
        }

        public string GetConnectionString()
        {
            // 解决非Web程序执行无法解析 HttpContext 问题
            if (_httpContextAccessor?.HttpContext == null) return default;

            var host = _httpContextAccessor.HttpContext.Request.Host.Value;
            // 建议缓存查询结果
            var isExistsKey = _memoryCache.TryGetValue(host, out object value);
            if (isExistsKey) return value?.ToString();
            else
            {
                var connectionString = _multipleTenantDbContext.GetConnectionString(host);
                _memoryCache.Set(host, connectionString);
                return connectionString;
            }
        }
    }
}
