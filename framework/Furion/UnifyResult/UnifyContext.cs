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

using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Furion.UnifyResult
{
    /// <summary>
    /// 规范化结果上下文
    /// </summary>
    [SuppressSniffer]
    public static class UnifyContext
    {
        /// <summary>
        /// 是否启用规范化结果
        /// </summary>
        internal static bool IsEnabledUnifyHandle = false;

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
        public static (int StatusCode, object ErrorCode, object Errors) GetExceptionMetadata(ExceptionContext context)
        {
            // 获取错误码
            var errorCode = context.Exception is AppFriendlyException friendlyException ? friendlyException?.ErrorCode : default;

            // 读取规范化状态码信息
            var statusCode = Get(UnifyResultStatusCodeKey) ?? StatusCodes.Status500InternalServerError;

            // 优先获取内部异常
            var errorMessage = context.Exception?.InnerException?.Message ?? context.Exception.Message;
            var validationFlag = "[Validation]";

            // 处理验证失败异常
            object errors = default;
            if (errorMessage.StartsWith(validationFlag))
            {
                // 处理结果
                errors = JSON.Deserialize<object>(errorMessage[validationFlag.Length..]);

                // 设置为400状态码
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                // 判断是否定义了全局类型异常
                var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

                // 查找所有全局定义异常
                var typeExceptionAttributes = actionDescriptor.MethodInfo
                            .GetCustomAttributes<IfExceptionAttribute>()
                            .Where(u => u.ErrorCode == null);

                // 处理全局异常
                if (typeExceptionAttributes.Any())
                {
                    // 首先判断是否有相同类型的异常
                    var actionIfExceptionAttribute = typeExceptionAttributes.FirstOrDefault(u => u.ExceptionType == context.Exception.GetType())
                            ?? typeExceptionAttributes.FirstOrDefault(u => u.ExceptionType == null);

                    if (actionIfExceptionAttribute is { ErrorMessage: not null }) errors = actionIfExceptionAttribute.ErrorMessage;
                }
                else errors = errorMessage;
            }

            return ((int)statusCode, errorCode, errors);
        }

        /// <summary>
        /// 填充附加信息
        /// </summary>
        /// <param name="extras"></param>
        public static void Fill(object extras)
        {
            var items = App.HttpContext?.Items;
            if (items.ContainsKey(UnifyResultExtrasKey)) items.Remove(UnifyResultExtrasKey);
            items.Add(UnifyResultExtrasKey, extras);
        }

        /// <summary>
        /// 读取附加信息
        /// </summary>
        public static object Take()
        {
            object extras = null;
            App.HttpContext?.Items?.TryGetValue(UnifyResultExtrasKey, out extras);
            return extras;
        }

        /// <summary>
        /// 设置响应状态码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="options"></param>
        public static void SetResponseStatusCodes(HttpContext context, int statusCode, UnifyResultStatusCodesOptions options)
        {
            // 篡改响应状态码
            if (options.AdaptStatusCodes != null && options.AdaptStatusCodes.Count > 0)
            {
                foreach (var (originStatusCode, descStatusCode) in options.AdaptStatusCodes)
                {
                    if (statusCode == originStatusCode)
                    {
                        context.Response.StatusCode = descStatusCode;
                        return;
                    }
                }
            }

            // 如果为 null，所有状态码设置为 200
            if (options.Return200StatusCodes == null) context.Response.StatusCode = 200;
            // 否则只有里面的才设置为 200
            else if (options.Return200StatusCodes.Contains(statusCode)) context.Response.StatusCode = 200;
            else { }
        }

        /// <summary>
        /// 设置规范化结果信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal static void Set(string key, object value)
        {
            var items = App.HttpContext?.Items;
            if (items != null && items.ContainsKey(key)) items.Remove(key);
            items?.Add(key, value);
        }

        /// <summary>
        /// 读取规范化结果信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static object Get(string key)
        {
            object value = null;
            App.HttpContext?.Items?.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// 是否跳过成功返回结果规范处理（状态码 200~209 ）
        /// </summary>
        /// <param name="method"></param>
        /// <param name="unifyResult"></param>
        /// <param name="isWebRequest"></param>
        /// <returns></returns>
        internal static bool IsSkipUnifyHandlerOnSucceedReturn(MethodInfo method, out IUnifyResultProvider unifyResult, bool isWebRequest = true)
        {
            // 判断是否跳过规范化处理
            var isSkip = !IsEnabledUnifyHandle
                  || method.GetRealReturnType().HasImplementedRawGeneric(RESTfulResultType)
                  || method.CustomAttributes.Any(x => typeof(NonUnifyAttribute).IsAssignableFrom(x.AttributeType) || typeof(ProducesResponseTypeAttribute).IsAssignableFrom(x.AttributeType) || typeof(IApiResponseMetadataProvider).IsAssignableFrom(x.AttributeType))
                  || method.ReflectedType.IsDefined(typeof(NonUnifyAttribute), true);

            if (!isWebRequest)
            {
                unifyResult = null;
                return isSkip;
            }

            unifyResult = isSkip ? null : App.GetService<IUnifyResultProvider>();
            return unifyResult == null || isSkip;
        }

        /// <summary>
        /// 是否跳过规范化处理（包括任意状态：成功，失败或其他状态码）
        /// </summary>
        /// <param name="method"></param>
        /// <param name="unifyResult"></param>
        /// <returns></returns>
        internal static bool IsSkipUnifyHandler(MethodInfo method, out IUnifyResultProvider unifyResult)
        {
            // 判断是否跳过规范化处理
            var isSkip = !IsEnabledUnifyHandle
                    || method.CustomAttributes.Any(x => typeof(NonUnifyAttribute).IsAssignableFrom(x.AttributeType))
                    || (
                            !method.CustomAttributes.Any(x => typeof(ProducesResponseTypeAttribute).IsAssignableFrom(x.AttributeType) || typeof(IApiResponseMetadataProvider).IsAssignableFrom(x.AttributeType))
                            && method.ReflectedType.IsDefined(typeof(NonUnifyAttribute), true)
                        );

            unifyResult = isSkip ? null : App.RootServices.GetService<IUnifyResultProvider>();
            return unifyResult == null || isSkip;
        }

        /// <summary>
        /// 是否跳过特定状态码规范化处理（如，处理 401，403 状态码情况）
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unifyResult"></param>
        /// <returns></returns>
        internal static bool IsSkipUnifyHandlerOnSpecifiedStatusCode(HttpContext context, out IUnifyResultProvider unifyResult)
        {
            // 获取终点路由特性
            var endpointFeature = context.Features.Get<IEndpointFeature>();
            if (endpointFeature == null)
            {
                unifyResult = null;
                return true;
            }

            // 判断是否跳过规范化处理
            var isSkip = !IsEnabledUnifyHandle
                    || context.GetMetadata<NonUnifyAttribute>() != null
                    || endpointFeature?.Endpoint?.Metadata?.GetMetadata<NonUnifyAttribute>() != null;

            unifyResult = isSkip ? null : context.RequestServices.GetService<IUnifyResultProvider>();
            return unifyResult == null || isSkip;
        }
    }
}