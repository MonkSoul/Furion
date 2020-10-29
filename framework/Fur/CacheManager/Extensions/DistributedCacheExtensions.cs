using Fur.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Caching.Distributed
{
    /// <summary>
    /// 分布式缓存扩展方法
    /// </summary>
    [SkipScan]
    public static class DistributedCacheExtensions
    {
        /// <summary>
        /// 根据缓存key获取缓存信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <returns>缓存结果信息</returns>
        public static TEntity Get<TEntity>(this IDistributedCache cache, string key)
        {
            try
            {
                var valueString = cache.GetString(key);

                if (string.IsNullOrEmpty(valueString))
                {
                    return default;
                }

                return JsonSerializer.Deserialize<TEntity>(valueString);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 根据缓存key获取缓存信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="token">取消异步令牌</param>
        /// <returns>缓存结果信息</returns>
        public static async Task<TEntity> GetAsync<TEntity>(this IDistributedCache cache, string key, CancellationToken token = default)
        {
            try
            {
                var valueString = await cache.GetStringAsync(key, token);

                if (string.IsNullOrEmpty(valueString))
                {
                    return default;
                }

                return JsonSerializer.Deserialize<TEntity>(valueString);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 根据缓存key获取缓存信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="value"></param>
        /// <returns>缓存结果信息</returns>
        public static bool TryGetValue<TEntity>(this IDistributedCache cache, string key, out TEntity value)
        {
            var valueString = cache.GetString(key);
            if (!string.IsNullOrEmpty(valueString))
            {
                value = JsonSerializer.Deserialize<TEntity>(valueString);
                return true;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// 根据缓存key设置缓存信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="value">待缓存信息</param>
        public static void Set<TEntity>(this IDistributedCache cache, string key, TEntity value)
        {
            cache.SetString(key, JsonSerializer.Serialize(value));
        }

        /// <summary>
        /// 根据缓存key设置缓存信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="value">待缓存信息</param>
        /// <param name="options">缓存过期配置</param>
        public static void Set<TEntity>(this IDistributedCache cache, string key, TEntity value, DistributedCacheEntryOptions options)
        {
            cache.SetString(key, JsonSerializer.Serialize(value), options);
        }

        /// <summary>
        /// 根据缓存key设置缓存信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="value">待缓存信息</param>
        /// <param name="token">取消异步令牌</param>
        /// <returns></returns>
        public static async Task SetAsync<TEntity>(this IDistributedCache cache, string key, TEntity value, CancellationToken token = default)
        {
            await cache.SetStringAsync(key, JsonSerializer.Serialize(value), token);
        }

        /// <summary>
        /// 根据缓存key设置缓存信息
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="value">待缓存信息</param>
        /// <param name="options">缓存过期配置</param>
        /// <param name="token">取消异步令牌</param>
        /// <returns></returns>
        public static async Task SetAsync<TEntity>(this IDistributedCache cache, string key, TEntity value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            await cache.SetStringAsync(key, JsonSerializer.Serialize(value), options, token);
        }

        /// <summary>
        /// 根据缓存key获取缓存信息，若不存在则创建缓存
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="factory">获取数据委托</param>
        /// <returns></returns>
        public static TEntity GetOrCreate<TEntity>(this IDistributedCache cache, string key, Func<TEntity> factory)
        {
            if (!cache.TryGetValue(key, out TEntity obj))
            {
                obj = factory();
                cache.Set(key, obj);
            }
            return obj;
        }

        /// <summary>
        /// 根据缓存key获取缓存信息，若不存在则创建缓存
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="factory">获取数据委托</param>
        /// <param name="options">缓存过期配置</param>
        /// <returns></returns>
        public static TEntity GetOrCreate<TEntity>(this IDistributedCache cache, string key, Func<TEntity> factory, DistributedCacheEntryOptions options)
        {
            if (!cache.TryGetValue(key, out TEntity obj))
            {
                obj = factory();
                cache.Set(key, obj, options);
            }
            return obj;
        }

        /// <summary>
        /// 根据缓存key获取缓存信息，若不存在则创建缓存
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="factory">获取数据委托</param>
        /// <returns></returns>
        public static async Task<TEntity> GetOrCreateAsync<TEntity>(this IDistributedCache cache, string key, Func<Task<TEntity>> factory)
        {
            if (!cache.TryGetValue(key, out TEntity obj))
            {
                obj = await factory();
                await cache.SetAsync(key, obj);
            }
            return obj;
        }

        /// <summary>
        /// 根据缓存key获取缓存信息，若不存在则创建缓存
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="cache">分布式缓存对象</param>
        /// <param name="key">缓存key</param>
        /// <param name="factory">获取数据委托</param>
        /// <param name="options">缓存过期配置</param>
        /// <returns></returns>
        public static async Task<TEntity> GetOrCreateAsync<TEntity>(this IDistributedCache cache, string key, Func<Task<TEntity>> factory, DistributedCacheEntryOptions options)
        {
            if (!cache.TryGetValue(key, out TEntity obj))
            {
                obj = await factory();
                await cache.SetAsync(key, obj, options);
            }
            return obj;
        }
    }
}