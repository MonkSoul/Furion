using Fur.DependencyInjection;
using Fur.UnifyResult;
using Microsoft.AspNetCore.Http;
using System;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 异常拓展
    /// </summary>
    [SkipScan]
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 设置异常状态码
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public static Exception StatusCode(this Exception exception, int statusCode = StatusCodes.Status500InternalServerError)
        {
            UnifyResultContext.Set(UnifyResultContext.UnifyResultStatusCodeKey, statusCode);
            return exception;
        }
    }
}