// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Fur.FriendlyException
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public interface IGlobalExceptionHandler
    {
        /// <summary>
        /// 异常拦截
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnExceptionAsync(ExceptionContext context);
    }
}