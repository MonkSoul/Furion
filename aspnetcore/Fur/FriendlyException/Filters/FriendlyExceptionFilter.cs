using Fur;
using StackExchange.Profiling;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Filters
{
    public sealed class FriendlyExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 标识异常已经被处理
            context.ExceptionHandled = true;

            // 设置异常结果
            var exception = context.Exception;
            context.Result = new ContentResult { Content = exception.Message };

            // 打印错误到 MiniProfiler 中
            PrintToMiniProfiler(context.Exception);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 打印错误到 MiniProfiler 中
        /// </summary>
        /// <param name="exception"></param>
        private static void PrintToMiniProfiler(Exception exception)
        {
            // 判断是否注入 MiniProfiler 组件
            if (App.Settings.InjectMiniProfiler != true) return;

            // 获取异常堆栈
            var traceFrame = new StackTrace(exception, true).GetFrame(0);

            // 获取出错的文件名
            var exceptionFileName = traceFrame.GetFileName();

            // 获取出错的行号
            var exceptionFileLineNumber = traceFrame.GetFileLineNumber();

            // 打印错误文件名和行号
            if (!string.IsNullOrEmpty(exceptionFileName) && exceptionFileLineNumber > 0)
            {
                MiniProfiler.Current.CustomTiming("errors", $"{exceptionFileName}:line {exceptionFileLineNumber}", "Locator").Errored = true;
            }

            // 打印完整的堆栈信息
            MiniProfiler.Current.CustomTiming("errors", exception.ToString(), "StackTrace").Errored = true;
        }
    }
}