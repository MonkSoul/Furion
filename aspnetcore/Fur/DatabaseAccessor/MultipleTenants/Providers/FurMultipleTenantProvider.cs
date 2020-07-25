using Fur.DatabaseAccessor.Contexts.Pools;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Fur.DatabaseAccessor.MultipleTenants.Providers
{
    /// <summary>
    /// Fur 框架默认提供的多租户提供器
    /// <para>说明：自定义多租户提供器必须在构造函数中将多租户数据库上下文保存到数据库上下文池中</para>
    /// </summary>
    public class FurMultipleTenantProvider : IMultipleTenantProvider
    {
        /// <summary>
        /// 多租户数据库上下文
        /// </summary>
        private readonly FurMultipleTenantDbContext _multipleTenantDbContext;

        /// <summary>
        /// HttpContext 访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 内存缓存
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        #region 构造函数 + public FurMultipleTenantProvider(FurMultipleTenantDbContext multipleTenantDbContext, IDbContextPool dbContextPool, IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="multipleTenantDbContext">多租户数据库上下文</param>
        /// <param name="dbContextPool">数据库上下文池</param>
        /// <param name="httpContextAccessor">HttpContext 访问器</param>
        /// <param name="memoryCache">内存缓存</param>
        public FurMultipleTenantProvider(FurMultipleTenantDbContext multipleTenantDbContext
            , IDbContextPool dbContextPool
            , IHttpContextAccessor httpContextAccessor
            , IMemoryCache memoryCache)
        {
            // 自定义多租户提供器必须将多租户数据库上下文保存到数据库上下文池中
            dbContextPool.SaveDbContext(multipleTenantDbContext);

            _multipleTenantDbContext = multipleTenantDbContext;
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
        }
        #endregion

        #region 获取租户Id + public int GetTenantId()
        /// <summary>
        /// 获取租户Id
        /// </summary>
        /// <returns>租户Id</returns>
        public int GetTenantId()
        {
            // 解决非Web程序执行无法解析 HttpContext 问题
            if (_httpContextAccessor?.HttpContext == null) return default;

            var host = _httpContextAccessor.HttpContext.Request.Host.Value;

            // 建议缓存查询结果
            var isExistsKey = _memoryCache.TryGetValue(host, out object value);
            if (isExistsKey) return Convert.ToInt32(value);
            else
            {
                var tenantId = _multipleTenantDbContext.GetTenantId(host);
                _memoryCache.Set(host, tenantId);
                return tenantId;
            }
        }
        #endregion
    }
}