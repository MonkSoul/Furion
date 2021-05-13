using Furion.DependencyInjection;
using Furion.JsonSerialization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Furion.SensitiveDetection
{
    /// <summary>
    /// 敏感词（脱敏）提供器（默认实现）
    /// </summary>
    [SkipScan]
    public class SensitiveDetectionProvider : ISensitiveDetectionProvider
    {
        /// <summary>
        /// 序列化提供器
        /// </summary>
        private readonly IJsonSerializerProvider _jsonSerializerProvider;

        /// <summary>
        /// 分布式缓存
        /// </summary>
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// 文件提供器（支持物理路径和嵌入资源）
        /// </summary>
        private readonly IFileProvider _fileProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="distributedCache"></param>
        /// <param name="jsonSerializerProvider"></param>
        /// <param name="fileProvider"></param>
        public SensitiveDetectionProvider(IDistributedCache distributedCache
            , IJsonSerializerProvider jsonSerializerProvider
            , IFileProvider fileProvider)
        {
            _distributedCache = distributedCache;
            _jsonSerializerProvider = jsonSerializerProvider;
            _fileProvider = fileProvider;
        }

        /// <summary>
        /// 分布式缓存键
        /// </summary>
        private const string DISTRIBUTED_KEY = "SENSITIVE:WORDS";

        /// <summary>
        /// 返回所有敏感词
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetWordsAsync()
        {
            // 读取缓存数据
            var wordsCached = await _distributedCache.GetStringAsync(DISTRIBUTED_KEY);
            if (wordsCached != null) return _jsonSerializerProvider.Deserialize<IEnumerable<string>>(wordsCached);

            // 读取文件内容
            byte[] buffer;
            using (Stream readStream = _fileProvider.GetFileInfo("sensitive-words.txt").CreateReadStream()) // 暂时不提供配置文件名称
            {
                buffer = new byte[readStream.Length];
                await readStream.ReadAsync(buffer.AsMemory(0, buffer.Length));
            }

            var content = Encoding.UTF8.GetString(buffer);

            // 取换行符分割字符串
            var words = content.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            // 缓存数据
            await _distributedCache.SetStringAsync(DISTRIBUTED_KEY, _jsonSerializerProvider.Serialize(words));

            return words;
        }

        /// <summary>
        /// 判断敏感词是否有效（自定义算法）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<bool> IsVaildAsync(string text)
        {
            var words = await GetWordsAsync();
            return !words.Any(u => u.Contains(text));
        }
    }
}