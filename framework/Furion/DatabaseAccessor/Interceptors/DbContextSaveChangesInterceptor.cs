using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文提交拦截器
    /// </summary>
    [SkipScan]
    public class DbContextSaveChangesInterceptor : SaveChangesInterceptor
    {
    }
}