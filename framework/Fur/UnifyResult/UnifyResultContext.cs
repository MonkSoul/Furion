using Fur.DependencyInjection;
using Fur.WebUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;
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
        /// 规范化结果状态码
        /// </summary>
        internal static string UnifyResultStatusCodeKey = "UNIFY_RESULT_STATUS_CODE";

        /// <summary>
        /// 获取异常元数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static (int ErrorCode, object ErrorObject) GetExceptionMetadata(ExceptionContext context)
        {
            var errorCode = Get(UnifyResultStatusCodeKey) ?? StatusCodes.Status500InternalServerError;

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

            return ((int)errorCode, errorObject);
        }

        /// <summary>
        /// 填充附加信息
        /// </summary>
        /// <param name="extras"></param>
        public static void Fill(object extras)
        {
            var items = HttpContextUtility.GetCurrentHttpContext()?.Items;
            if (items.ContainsKey(UnifyResultExtrasKey)) items.Remove(UnifyResultExtrasKey);
            items.Add(UnifyResultExtrasKey, extras);
        }

        /// <summary>
        /// 读取附加信息
        /// </summary>
        public static object Take()
        {
            object extras = null;
            HttpContextUtility.GetCurrentHttpContext()?.Items?.TryGetValue(UnifyResultExtrasKey, out extras);
            return extras;
        }

        /// <summary>
        /// 设置规范化结果信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            var items = HttpContextUtility.GetCurrentHttpContext()?.Items;
            if (items.ContainsKey(key)) items.Remove(key);
            items.Add(key, value);
        }

        /// <summary>
        /// 读取规范化结果信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            object value = null;
            HttpContextUtility.GetCurrentHttpContext()?.Items?.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// 是否跳过成功结果规范处理
        /// </summary>
        /// <param name="method"></param>
        /// <param name="unifyResult"></param>
        /// <returns></returns>
        internal static bool IsSkipOnSuccessUnifyHandler(MethodInfo method, out IUnifyResultProvider unifyResult)
        {
            // 判断是否手动添加了标注或跳过规范化处理
            var isSkip = method.CustomAttributes.Any(x => typeof(NonUnifyAttribute).IsAssignableFrom(x.AttributeType) || typeof(ProducesResponseTypeAttribute).IsAssignableFrom(x.AttributeType)
                  || typeof(IApiResponseMetadataProvider).IsAssignableFrom(x.AttributeType));

            unifyResult = isSkip ? null : App.GetService<IUnifyResultProvider>();
            return unifyResult == null || isSkip;
        }

        /// <summary>
        /// 是否跳过规范化处理
        /// </summary>
        /// <param name="method"></param>
        /// <param name="unifyResult"></param>
        /// <returns></returns>
        internal static bool IsSkipUnifyHandler(MethodInfo method, out IUnifyResultProvider unifyResult)
        {
            // 判断是否跳过规范化处理
            var isSkip = method.CustomAttributes.Any(x => typeof(NonUnifyAttribute).IsAssignableFrom(x.AttributeType));

            unifyResult = isSkip ? null : App.GetService<IUnifyResultProvider>();
            return unifyResult == null || isSkip;
        }

        /// <summary>
        /// 是否跳过规范化处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unifyResult"></param>
        /// <returns></returns>
        internal static bool IsSkipUnifyHandler(HttpContext context, out IUnifyResultProvider unifyResult)
        {
            // 判断是否跳过规范化处理
            var isSkip = context.GetMetadata<NonUnifyAttribute>() != null;

            unifyResult = isSkip ? null : App.GetService<IUnifyResultProvider>();
            return unifyResult == null || isSkip;
        }
    }
}