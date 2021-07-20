// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.JsonSerialization;
using Furion.Templates.Extensions;
using Furion.VirtualFileServer;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Furion.SensitiveDetection
{
    /// <summary>
    /// 脱敏词汇（脱敏）提供器（默认实现）
    /// </summary>
    [SuppressSniffer]
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
        /// <param name="fileProviderResolve"></param>
        public SensitiveDetectionProvider(IDistributedCache distributedCache
            , IJsonSerializerProvider jsonSerializerProvider
            , Func<FileProviderTypes, object, IFileProvider> fileProviderResolve)
        {
            _distributedCache = distributedCache;
            _jsonSerializerProvider = jsonSerializerProvider;
            _fileProvider = fileProviderResolve(FileProviderTypes.Embedded, Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// 分布式缓存键
        /// </summary>
        private const string DISTRIBUTED_KEY = "SENSITIVE:WORDS";

        /// <summary>
        /// 返回所有脱敏词汇
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetWordsAsync()
        {
            // 读取缓存数据
            var wordsCached = await _distributedCache.GetStringAsync(DISTRIBUTED_KEY);
            if (wordsCached != null) return _jsonSerializerProvider.Deserialize<IEnumerable<string>>(wordsCached);

            // 读取文件内容
            byte[] buffer;
            using (var readStream = _fileProvider.GetFileInfo("sensitive-words.txt").CreateReadStream()) // 暂时不提供配置文件名称
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
        /// 判断脱敏词汇是否有效（支持自定义算法）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<bool> VaildedAsync(string text)
        {
            // 空字符串和空白字符不验证
            if (string.IsNullOrWhiteSpace(text)) return true;

            // 查找脱敏词汇出现次数和位置
            var foundSets = await FoundSensitiveWordsAsync(text);

            return foundSets.Count == 0;
        }

        /// <summary>
        /// 替换敏感词汇
        /// </summary>
        /// <param name="text"></param>
        /// <param name="transfer"></param>
        /// <returns></returns>
        public async Task<string> ReplaceAsync(string text, char transfer = '*')
        {
            if (string.IsNullOrWhiteSpace(text)) return default;

            // 查找脱敏词汇出现次数和位置
            var foundSets = await FoundSensitiveWordsAsync(text);

            // 如果没有敏感词则返回原字符串
            if (foundSets.Count == 0) return text;

            var stringBuilder = new StringBuilder(text);

            // 循环替换
            foreach (var kv in foundSets)
            {
                for (var i = 0; i < kv.Value.Count; i++)
                {
                    for (var j = 0; j < kv.Key.Length; j++)
                    {
                        var tempIndex = GetSensitiveWordIndex(kv.Value, i, kv.Key.Length);

                        // 设置替换的字符
                        stringBuilder[tempIndex + j] = transfer;
                    }
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 查找脱敏词汇
        /// </summary>
        /// <param name="text"></param>
        private async Task<Dictionary<string, List<int>>> FoundSensitiveWordsAsync(string text)
        {
            // 支持读取配置渲染
            var realText = text.Render();

            // 获取词库
            var sensitiveWords = await GetWordsAsync();

            var stringBuilder = new StringBuilder(realText);
            var tempStringBuilder = new StringBuilder();

            // 记录脱敏词汇出现位置和次数
            int findIndex;
            var foundSets = new Dictionary<string, List<int>>();

            // 遍历所有脱敏词汇并查找字符串是否包含
            foreach (var sensitiveWord in sensitiveWords)
            {
                // 重新填充目标字符串
                tempStringBuilder.Clear();
                tempStringBuilder.Append(stringBuilder);

                // 查询查找至结尾
                while (tempStringBuilder.ToString().Contains(sensitiveWord))
                {
                    if (foundSets.ContainsKey(sensitiveWord) == false)
                    {
                        foundSets.Add(sensitiveWord, new List<int>());
                    }

                    findIndex = tempStringBuilder.ToString().IndexOf(sensitiveWord);
                    foundSets[sensitiveWord].Add(findIndex);

                    // 删除从零开始，长度为 findIndex + sensitiveWord.Length 的字符串
                    tempStringBuilder.Remove(0, findIndex + sensitiveWord.Length);
                }
            }

            return foundSets;
        }

        /// <summary>
        /// 获取敏感词索引
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <param name="sensitiveWordLength"></param>
        /// <returns></returns>
        private static int GetSensitiveWordIndex(List<int> list, int count, int sensitiveWordLength)
        {
            // 用于返回当前敏感词的第 count 个的真实索引
            var sum = 0;
            for (var i = 0; i <= count; i++)
            {
                if (i == 0)
                {
                    sum = list[i];
                }
                else
                {
                    sum += list[i] + sensitiveWordLength;
                }
            }
            return sum;
        }
    }
}