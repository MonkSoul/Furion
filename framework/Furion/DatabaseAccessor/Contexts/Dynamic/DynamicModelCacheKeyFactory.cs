// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 动态模型缓存工厂
/// </summary>
/// <remarks>主要用来实现数据库分表分库</remarks>
[SuppressSniffer]
public class DynamicModelCacheKeyFactory : IModelCacheKeyFactory
{
    /// <summary>
    /// 动态模型缓存Key
    /// </summary>
    private static int cacheKey;

    /// <summary>
    /// 重写构建模型
    /// </summary>
    /// <remarks>动态切换表之后需要调用该方法</remarks>
    public static void RebuildModels()
    {
        Interlocked.Increment(ref cacheKey);
    }

#if NET5_0

    /// <summary>
    /// 更新模型缓存
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public object Create(DbContext context)
    {
        return (context.GetType(), cacheKey);
    }

#else
    /// <summary>
    /// 更新模型缓存
    /// </summary>
    /// <param name="context"></param>
    /// <param name="designTime"></param>
    /// <returns></returns>
    public object Create(DbContext context, bool designTime)
    {
        var dbContextAttribute = DbProvider.GetAppDbContextAttribute(context.GetType());

        return dbContextAttribute?.Mode == DbContextMode.Dynamic
            ? (context.GetType(), cacheKey, designTime)
            : context.GetType();
    }
#endif
}