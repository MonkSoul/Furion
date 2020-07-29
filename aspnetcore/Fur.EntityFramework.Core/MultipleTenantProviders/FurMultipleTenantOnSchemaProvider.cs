using Fur.DatabaseAccessor.MultipleTenants.Providers;
using Fur.EntityFramework.Core.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Fur.EntityFramework.Core.MultipleTenantProviders
{
    // 目前基于Schema方式不支持code first 迁移
    public class FurMultipleTenantOnSchemaProvider : IMultipleTenantOnSchemaProvider
    {
        private readonly FurMultipleTenantDbContext _multipleTenantDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;

        public FurMultipleTenantOnSchemaProvider(FurMultipleTenantDbContext multipleTenantDbContext
            , IHttpContextAccessor httpContextAccessor
            , IMemoryCache memoryCache)
        {
            _multipleTenantDbContext = multipleTenantDbContext;
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
        }

        public string GetSchema()
        {
            // 解决非Web程序执行无法解析 HttpContext 问题
            if (_httpContextAccessor?.HttpContext == null) return default;

            var host = _httpContextAccessor.HttpContext.Request.Host.Value;
            // 建议缓存查询结果
            var isExistsKey = _memoryCache.TryGetValue(host, out object value);
            if (isExistsKey) return value?.ToString();
            else
            {
                var schema = _multipleTenantDbContext.GetSchema(host);
                _memoryCache.Set(host, schema);
                return schema;
            }
        }
    }
}
