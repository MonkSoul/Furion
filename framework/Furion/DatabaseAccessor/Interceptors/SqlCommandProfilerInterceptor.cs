using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 数据库执行命令拦截
    /// </summary>
    [SkipScan]
    internal sealed class SqlCommandProfilerInterceptor : DbCommandInterceptor
    {
    }
}