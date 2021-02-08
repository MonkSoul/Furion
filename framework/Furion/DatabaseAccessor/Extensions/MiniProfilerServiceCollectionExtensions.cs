using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using StackExchange.Profiling.Internal;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// MiniProfiler 服务拓展类
    /// </summary>
    [SkipScan]
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