using Fur.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text.Json;

namespace Fur.UnifyResult
{
    /// <summary>
    /// 规范化结果上下文
    /// </summary>
    [SkipScan]
    public static class UnifyResultContext
    {
        /// <summary>
        /// 规范化结果类型
        /// </summary>
        internal static Type RESTfulResultType = typeof(RESTfulResult<>);

        /// <summary>
        /// 获取异常元数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static (int ErrorCode, object ErrorObject) GetExceptionMetadata(ExceptionContext context)
        {
            var errorCode = StatusCodes.Status500InternalServerError;
            var errorMessage = context.Exception.Message;
            var validationFlag = "[Validation]";

            // 处理验证失败异常
            object errorObject;
            if (errorMessage.StartsWith(validationFlag))
            {
                // 处理结果
                errorObject = JsonSerializer.Deserialize<object>(errorMessage[validationFlag.Length..]);

                errorCode = StatusCodes.Status400BadRequest;
            }
            else errorObject = errorMessage;

            return (errorCode, errorObject);
        }
    }
}