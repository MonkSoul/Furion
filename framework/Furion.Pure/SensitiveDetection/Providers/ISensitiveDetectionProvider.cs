// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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