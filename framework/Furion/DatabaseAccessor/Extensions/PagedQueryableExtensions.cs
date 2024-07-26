// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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
    /// <param name="totalComputed">总数计算方式</param>
    /// <returns></returns>
    public static PagedList<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20, Func<int> totalComputed = null)
    {
        if (pageIndex <= 0) throw new InvalidOperationException($"{nameof(pageIndex)} must be a positive integer greater than 0.");

        var totalCount = totalComputed == null
            ? entities.Count()
            : totalComputed();

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
    /// <param name="totalComputed">总数计算方式</param>
    /// <returns></returns>
    public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default, Func<int> totalComputed = null)
    {
        if (pageIndex <= 0) throw new InvalidOperationException($"{nameof(pageIndex)} must be a positive integer greater than 0.");

        var totalCount = totalComputed == null
            ? (await entities.CountAsync(cancellationToken))
            : totalComputed();

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