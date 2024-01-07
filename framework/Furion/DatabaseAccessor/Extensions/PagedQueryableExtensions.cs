// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.EntityFrameworkCore;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 分页拓展类
/// </summary>
[SuppressSniffer]
public static class PagedQueryableExtensions
{
    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static PagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20)
    {
        if (pageIndex <= 0) throw new InvalidOperationException($"{nameof(pageIndex)} must be a positive integer greater than 0.");

        var totalCount = entities.Count();
        var items = entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedList<TEntity>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNextPages = pageIndex < totalPages,
            HasPrevPages = pageIndex - 1 > 0
        };
    }

    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default)
    {
        if (pageIndex <= 0) throw new InvalidOperationException($"{nameof(pageIndex)} must be a positive integer greater than 0.");

        var totalCount = await entities.CountAsync(cancellationToken);
        var items = await entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedList<TEntity>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNextPages = pageIndex < totalPages,
            HasPrevPages = pageIndex - 1 > 0
        };
    }
}