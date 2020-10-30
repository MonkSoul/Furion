using Fur.DependencyInjection;
using System;

namespace Fur.UnifyResult
{
    /// <summary>
    /// 规范化结果上下文
    /// </summary>
    [SkipScan]
    internal static class UnifyResultContext
    {
        /// <summary>
        /// 规范化结果类型
        /// </summary>
        internal static Type RESTfulResultType = typeof(RESTfulResult<>);
    }
}