using Fur.FriendlyException.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using StackExchange.Profiling;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.FriendlyException.Filters
{
    public class ExceptionAsyncFilter : IAsyncExceptionFilter
    {
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
        private static void ConvertExceptionInfo(ExceptionContext context, ControllerActionDescriptor descriptor, out string exceptionMessage, out string exceptionErrorString)
        {
            var exception = context.Exception;
            var method = descriptor.MethodInfo;

            exceptionMessage = exception.Message;
            exceptionErrorString = exception.ToString();

            if (method.IsDefined(typeof(IfExceptionAttribute), false))
            {
                var ifExceptionAttributes = method.GetCustomAttributes<IfExceptionAttribute>(false);
                if (exceptionMessage.StartsWith("##") && exceptionMessage.EndsWith("##"))
                {
                    var code = exceptionMessage[2..^2];

                    var exceptionConvert = ifExceptionAttributes.FirstOrDefault(u => u.ExceptionCode.ToString() == code);
                    if (exceptionConvert != null)
                    {
                        exceptionMessage = exceptionMessage.Replace($"##{code}##", $"[{code}] {exceptionConvert.ExceptionMessage}");
                        exceptionErrorString = exceptionErrorString
                            .Replace($"System.Exception: {exception.Message}", $"{exceptionConvert.Exception.FullName}: {exception.Message}")
                            .Replace($"##{code}##", $"[{code}] {exceptionConvert.ExceptionMessage}");
                    }
                }
            }
        }
        #endregion
    }
}
