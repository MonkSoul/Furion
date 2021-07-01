// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using StackExchange.Profiling.Internal;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// MiniProfiler 服务拓展类
    /// </summary>
    [SuppressSniffer]
    public static class MiniProfilerServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 EF Core 进程监听拓展
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMiniProfilerBuilder AddRelationalDiagnosticListener(this IMiniProfilerBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.Services.AddSingleton<IMiniProfilerDiagnosticListener, RelationalDiagnosticListener>();

            return builder;
        }
    }
}