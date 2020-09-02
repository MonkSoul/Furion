// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Fur.FriendlyException;
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
            Oops.PrintToMiniProfiler(context.Exception);

            return Task.CompletedTask;
        }
    }
}