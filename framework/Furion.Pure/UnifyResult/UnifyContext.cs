// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.Templates.Extensions;
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

                    if (actionIfExceptionAttribute is { ErrorMessage: not null }) errors = actionIfExceptionAttribute.ErrorMessage.Render();
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
                var adaptStatusCode = options.AdaptStatusCodes.FirstOrDefault(u => u.Key == statusCode);
                if (adaptStatusCode.Key > 0)
                {
                    context.Response.StatusCode = adaptStatusCode.Value;
                    return;
                }
            }

            // 如果为 null，则所有请求错误的状态码设置为 200
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
        /// 检查请求成功是否进行规范化处理
        /// </summary>
        /// <param name="method"></param>
        /// <param name="unifyResult"></param>
        /// <param name="isWebRequest"></param>
        /// <returns>返回 true 跳过处理，否则进行规范化处理</returns>
        internal static bool CheckSucceeded(MethodInfo method, out IUnifyResultProvider unifyResult, bool isWebRequest = true)
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

            unifyResult = isSkip ? null : App.RootServices.GetService<IUnifyResultProvider>();
            return unifyResult == null || isSkip;
        }

        /// <summary>
        /// 检查请求失败（验证失败、抛异常）是否进行规范化处理
        /// </summary>
        /// <param name="method"></param>
        /// <param name="unifyResult"></param>
        /// <returns>返回 true 跳过处理，否则进行规范化处理</returns>
        internal static bool CheckFailed(MethodInfo method, out IUnifyResultProvider unifyResult)
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
        /// 检查短路状态码（>=400）是否进行规范化处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unifyResult"></param>
        /// <param name="intercept404StatusCodes"></param>
        /// <returns>返回 true 跳过处理，否则进行规范化处理</returns>
        internal static bool CheckStatusCode(HttpContext context, bool intercept404StatusCodes, out IUnifyResultProvider unifyResult)
        {
            IEndpointFeature endpointFeature = default;

            // 处理 404 问题
            if (!(intercept404StatusCodes && context.Response.StatusCode == StatusCodes.Status404NotFound))
            {
                // 获取终点路由特性
                endpointFeature = context.Features.Get<IEndpointFeature>();
                if (endpointFeature == null) return (unifyResult = null) == null;
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