// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.Reflection;
using Furion.Templates.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Furion.SensitiveDetection;

/// <summary>
/// 脱敏词汇（脱敏）提供器（默认实现）
/// </summary>
[SuppressSniffer]
public class SensitiveDetectionProvider : ISensitiveDetectionProvider
{
    /// <summary>
    /// 分布式缓存
    /// </summary>
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="distributedCache"></param>
    public SensitiveDetectionProvider(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
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
        if (wordsCached != null) return wordsCached.Split(new[] { "\r\n", "|" }, StringSplitOptions.RemoveEmptyEntries).Select(u => u.Trim());

        var entryAssembly = Reflect.GetEntryAssembly();

        // 解析嵌入式文件流
        byte[] buffer;
        using (var readStream = entryAssembly.GetManifestResourceStream($"{Reflect.GetAssemblyName(entryAssembly)}.sensitive-words.txt"))
        {
            buffer = new byte[readStream.Length];
            await readStream.ReadAsync(buffer.AsMemory(0, buffer.Length));
        }

        // 同时兼容 UTF-8 BOM，UTF-8
        string content;
        using (var stream = new MemoryStream(buffer))
        {
            using var streamReader = new StreamReader(stream, new UTF8Encoding(false));
            content = streamReader.ReadToEnd();
        }

        // 缓存数据
        await _distributedCache.SetStringAsync(DISTRIBUTED_KEY, content);

        // 取换行符分割字符串
        var words = content.Split(new[] { "\r\n", "|" }, StringSplitOptions.RemoveEmptyEntries)
                                          .Select(u => u.Trim())
                                          .Distinct();

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
                    var tempIndex = GetSensitiveWordIndex(kv.Value, i, sensitiveWordLength: kv.Key.Length);

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
            while (tempStringBuilder.ToString().IndexOf(sensitiveWord) > -1)
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