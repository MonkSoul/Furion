using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using StackExchange.Profiling;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.FriendlyException.Filters
{
    public class ExceptionAsyncFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// autofac 生命周期对象
        /// </summary>
        ILifetimeScope _lifetimeScope;

        /// <summary>
        /// 异常提供器
        /// </summary>
        IExceptionCodesProvider _exceptionCodesProvider;

        #region 构造函数 + public ExceptionAsyncFilter(ILifetimeScope lifetimeScope)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lifetimeScope">autofac 生命周期对象</param>
        public ExceptionAsyncFilter(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
        #endregion

        #region 异常异步拦截器 + public Task OnExceptionAsync(ExceptionContext context)
        /// <summary>
        /// 异常异步拦截器
        /// </summary>
        /// <param name="context">异常上下文</param>
        /// <returns><see cref="Task"/></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            ConvertExceptionInfo(context, descriptor, out string exceptionMessage, out string exceptionErrorString);

            context.Result = new ContentResult() { Content = exceptionMessage, StatusCode = StatusCodes.Status500InternalServerError };

            MiniProfiler.Current.CustomTiming("errors", exceptionErrorString, "Throw").Errored = true;
            return Task.CompletedTask;
        }
        #endregion

        #region 转换异常信息 + private static void ConvertExceptionInfo(ExceptionContext context, ControllerActionDescriptor descriptor, out string exceptionMessage, out string exceptionErrorString)
        /// <summary>
        /// 转换异常信息
        /// </summary>
        /// <param name="context">异常上下文</param>
        /// <param name="descriptor">控制器描述器</param>
        /// <param name="exceptionMessage">异常信息</param>
        /// <param name="exceptionErrorString">异常堆栈</param>
        private void ConvertExceptionInfo(ExceptionContext context, ControllerActionDescriptor descriptor, out string exceptionMessage, out string exceptionErrorString)
        {
            var exception = context.Exception;
            var method = descriptor.MethodInfo;

            exceptionMessage = exception.Message;
            exceptionErrorString = exception.ToString();

            if (exceptionMessage.StartsWith("##") && exceptionMessage.EndsWith("##"))
            {
                var customExceptionContent = exceptionMessage[2..^2];
                var codeAndType = customExceptionContent.Split(';', System.StringSplitOptions.RemoveEmptyEntries);

                var code = int.Parse(codeAndType[0]);
                var exceptionType = codeAndType[1];

                if (_lifetimeScope.IsRegistered<IExceptionCodesProvider>())
                {
                    _exceptionCodesProvider = _lifetimeScope.Resolve<IExceptionCodesProvider>();
                }

                var exceptionCodes = _exceptionCodesProvider?.GetExceptionCodes() ?? new Dictionary<int, string>();
                var exceptionMsg = exceptionCodes.ContainsKey(code) ? exceptionCodes[code] : "Internal Server Error.";

                exceptionMessage = exceptionMessage.Replace($"##{customExceptionContent}##", $"[{code}] {exceptionMsg}");
                exceptionErrorString = exceptionErrorString
                    .Replace($"System.Exception: {exception.Message}", $"{exceptionType}: {exception.Message}")
                    .Replace($"##{customExceptionContent}##", $"[{code}] {exceptionMsg}");
            }
        }
        #endregion
    }
}
