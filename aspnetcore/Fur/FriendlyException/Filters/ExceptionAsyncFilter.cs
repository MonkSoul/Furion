using Autofac;
using Fur.DependencyInjection.Extensions;
using Fur.Extensions;
using Fur.FriendlyException.Attributes;
using Fur.UnifyResult.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.FriendlyException.Filters
{
    public sealed class ExceptionAsyncFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// autofac 生命周期对象
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        private readonly IMemoryCache _memoryCache;
        private readonly IUnifyResultProvider _unifyResultProvider;

        /// <summary>
        /// 异常提供器
        /// </summary>
        private IExceptionProvider _exceptionCodesProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lifetimeScope">autofac 生命周期对象</param>
        /// <param name="memoryCache">内存缓存</param>
        public ExceptionAsyncFilter(ILifetimeScope lifetimeScope
            , IMemoryCache memoryCache
            , IUnifyResultProvider unifyResultProvider)
        {
            _lifetimeScope = lifetimeScope;
            _memoryCache = memoryCache;
            _unifyResultProvider = unifyResultProvider;
        }

        /// <summary>
        /// 异常异步拦截器
        /// </summary>
        /// <param name="context">异常上下文</param>
        /// <returns><see cref="Task"/></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var isAnonymouseRequest = descriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), false) || descriptor.ControllerTypeInfo.IsDefined(typeof(AllowAnonymousAttribute), false);
            var unAuthorizedRequest = isAnonymouseRequest || Convert.ToBoolean(context.HttpContext.Response.Headers["UnAuthorizedRequest"]);

            int statusCode = CombineExceptionInfo(context.Exception, descriptor.MethodInfo, out string exceptionMessage, out string exceptionErrorString);

            context.Result = _unifyResultProvider.UnifyExceptionResult(context, exceptionMessage, exceptionErrorString, unAuthorizedRequest, statusCode);

            var traceFrame = new StackTrace(context.Exception, true).GetFrame(0);
            var exceptionFileName = traceFrame.GetFileName();
            var exceptionFileLineNumber = traceFrame.GetFileLineNumber();
            if (exceptionFileName.HasValue() && exceptionFileLineNumber > 0)
            {
                MiniProfiler.Current.CustomTiming("errors", $"{exceptionFileName}:line {exceptionFileLineNumber}", "Locator").Errored = true;
            }

            MiniProfiler.Current.CustomTiming("errors", exceptionErrorString, "StackTrace").Errored = true;

            return Task.CompletedTask;
        }

        private int CombineExceptionInfo(Exception exception, MethodInfo method, out string exceptionMessage, out string exceptionString)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            exceptionMessage = exception.Message;
            exceptionString = exception.ToString();

            if (exceptionMessage.StartsWith("[[") && exceptionMessage.EndsWith("]]"))
            {
                var exceptionCode = int.Parse(exceptionMessage[2..^2]);
                var oopsModel = Oops.ExceptionOopsModels.GetValueOrDefault((method, exceptionCode));
                statusCode = oopsModel.StatusCode;

                var expMessage = "Internal Server Error.";
                if (oopsModel.IfException != null)
                {
                    expMessage = string.Format($"[{exceptionCode}] {oopsModel.IfException.Message}", oopsModel.Args);
                }
                else
                {
                    var exceptionCodes = LoadExceptionCodes(expMessage);
                    if (exceptionCodes.ContainsKey(exceptionCode))
                    {
                        expMessage = string.Format($"[{exceptionCode}] {exceptionCodes[exceptionCode]}", oopsModel.Args);
                    }
                }

                if (oopsModel.ExceptionType != null)
                {
                    exceptionString = exceptionString.Replace($"System.Exception: {exceptionMessage}", $"{oopsModel.ExceptionType}: {expMessage}");
                }
                else
                {
                    exceptionString = exceptionString.Replace($"{exceptionMessage}", $"{expMessage}");
                }
                exceptionMessage = expMessage;
            }

            return statusCode;
        }

        /// <summary>
        /// 加载异常状态码
        /// </summary>
        /// <param name="defaultExceptionMsg">默认异常</param>
        /// <returns><see cref="Dictionary{TKey, TValue}"/></returns>
        private Dictionary<int, string> LoadExceptionCodes(string defaultExceptionMsg = "Internal Server Error.")
        {
            var exceptionCodes = new Dictionary<int, string>();
            _exceptionCodesProvider = _lifetimeScope.GetService<IExceptionProvider>();
            if (_exceptionCodesProvider == null) return exceptionCodes;

            var exceptionCodesType = _exceptionCodesProvider.ExceptionCodesType();
            var isExistsKey = _memoryCache.TryGetValue(exceptionCodesType.FullName, out object codes);

            if (isExistsKey) return codes as Dictionary<int, string>;

            var fields = _exceptionCodesProvider.ExceptionCodesType().GetFields(BindingFlags.Public | BindingFlags.Static);
            if (fields.Length == 0) return exceptionCodes;

            foreach (var field in fields)
            {
                int fieldValue = Convert.ToInt32(field.GetRawConstantValue());
                string metaMessage = null;
                if (field.IsDefined(typeof(ExceptionMetaAttribute)))
                {
                    metaMessage = field.GetCustomAttribute<ExceptionMetaAttribute>().Message;
                }
                exceptionCodes.Add(fieldValue, metaMessage ?? defaultExceptionMsg);
            }

            _memoryCache.Set(exceptionCodesType.FullName, exceptionCodes);

            return exceptionCodes;
        }
    }
}