using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furion.SensitiveDetection
{
    /// <summary>
    /// 敏感词（脱敏）提供器
    /// </summary>
    public interface ISensitiveDetectionProvider
    {
        /// <summary>
        /// 返回所有敏感词
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetWordsAsync();

        /// <summary>
        /// 判断敏感词是否有效（自定义算法）
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task<bool> IsVaildAsync(string text);
    }
}