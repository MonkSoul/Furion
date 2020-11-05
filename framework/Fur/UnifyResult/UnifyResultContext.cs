using Fur.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
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
        /// 规范化结果额外数据键
        /// </summary>
        internal static string UnifyResultExtrasKey = "UNIFY_RESULT_EXTRAS";

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

        /// <summary>
        /// 填充附加信息
        /// </summary>
        /// <param name="extras"></param>
        public static void Fill(object extras)
        {
            var items = App.ApplicationServices.GetService<IHttpContextAccessor>()?.HttpContext?.Items;
            if (items.ContainsKey(UnifyResultExtrasKey)) items.Remove(UnifyResultExtrasKey);
            items.Add(UnifyResultExtrasKey, extras);
        }

        /// <summary>
        /// 读取附加信息
        /// </summary>
        public static object Take()
        {
            object extras = null;
            App.ApplicationServices.GetService<IHttpContextAccessor>()?.HttpContext?.Items?.TryGetValue(UnifyResultExtrasKey, out extras);
            return extras;
        }
    }
}