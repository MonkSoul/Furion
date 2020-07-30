using Fur.AppBasic.Attributes;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Fur.DatabaseAccessor.Interceptors
{
    /// <summary>
    /// 数据库执行命令拦截器
    /// <para>用来拦截数据库执行命令的每一个生命周期</para>
    /// </summary>
    [NonWrapper]
    public sealed class SqlCommandProfilerInterceptor : DbCommandInterceptor
    {
    }
}