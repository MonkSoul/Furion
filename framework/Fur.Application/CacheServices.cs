using Fur.DynamicApiController;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Fur.Application
{
    public class CacheServices : IDynamicApiController
    {
        private const string _timeCacheKey = "cache_time";

        private readonly IMemoryCache _memoryCache;

        public CacheServices(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [ApiDescriptionSettings(KeepName = true)]
        public DateTimeOffset GetOrCreate()
        {
            return _memoryCache.GetOrCreate(_timeCacheKey, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);  // 滑动缓存时间
                return DateTimeOffset.UtcNow;
            });
        }
    }
}