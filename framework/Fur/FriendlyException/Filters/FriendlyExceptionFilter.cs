using Fur.DependencyInjection;
using Fur.FriendlyException;
using Fur.UnifyResult;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Filters
{
    /// <summary>
    /// 友好异常拦截器
    /// </summary>
    [SkipScan]
    public sealed class FriendlyExceptionFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public FriendlyExceptionFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 异常拦截
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            // 解析异常处理服务，实现自定义异常额外操作，如记录日志等
            var globalExceptionHandler = _serviceProvider.GetService<IGlobalExceptionHandler>();
            if (globalExceptionHandler != null)
            {
                await globalExceptionHandler.OnExceptionAsync(context);
            }

            // 排除 Mvc 视图
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor.ControllerTypeInfo.BaseType == typeof(Controller)) return;

            // 标识异常已经被处理
            context.ExceptionHandled = true;

            // 设置异常结果
            var exception = context.Exception;

            // 处理规范化结果
            var unifyResult = _serviceProvider.GetService<IUnifyResultProvider>();
            context.Result = unifyResult == null ? new ContentResult { Content = exception.Message } : unifyResult.OnException(context);

            // 打印错误到 MiniProfiler 中
            Oops.PrintToMiniProfiler(context.Exception);
        }
    }
}