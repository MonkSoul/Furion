using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.SensitiveDetection
{
    /// <summary>
    /// 脱敏词汇（脱敏）提供器
    /// </summary>
    public interface ISensitiveDetectionProvider
    {
        /// <summary>
        /// 返回所有脱敏词汇
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetWordsAsync();

        /// <summary>
        /// 判断脱敏词汇是否有效（支持自定义算法）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task<bool> VaildedAsync(string text);

        /// <summary>
        /// 替换敏感词汇
        /// </summary>
        /// <param name="text"></param>
        /// <param name="transfer"></param>
        /// <returns></returns>
        Task<string> ReplaceAsync(string text, char transfer = '*');
    }
}