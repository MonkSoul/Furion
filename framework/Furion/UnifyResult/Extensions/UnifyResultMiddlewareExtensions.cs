// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.6.6
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion 
//          Github：https://github.com/monksoul/Furion 
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.UnifyResult;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 状态码中间件拓展
    /// </summary>
    [SkipScan]
    public static class UnifyResultMiddlewareExtensions
    {
        /// <summary>
        /// 添加状态码拦截中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="optionsBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseUnifyResultStatusCodes(this IApplicationBuilder builder, Action<UnifyResultStatusCodesOptions> optionsBuilder = default)
        {
            // 提供配置
            var options = new UnifyResultStatusCodesOptions();
            optionsBuilder?.Invoke(options);

            builder.UseMiddleware<UnifyResultStatusCodesMiddleware>(options);

            return builder;
        }
    }
}