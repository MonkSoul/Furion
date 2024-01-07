// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.DatabaseAccessor;
using StackExchange.Profiling.Internal;

namespace Microsoft.Extensions.DependencyInjection;

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