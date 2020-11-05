using Fur.DatabaseAccessor;
using Fur.DependencyInjection;
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
        /// 添加 EF Core 监听拓展
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMiniProfilerBuilder AddEntityFramework(this IMiniProfilerBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.Services.AddSingleton<IMiniProfilerDiagnosticListener, RelationalDiagnosticListener>();

            return builder;
        }
    }
}