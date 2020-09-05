// --------------------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur 
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// --------------------------------------------------------------------------------------

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