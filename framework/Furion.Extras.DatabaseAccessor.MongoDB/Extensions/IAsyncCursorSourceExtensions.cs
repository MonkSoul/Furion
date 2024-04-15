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

using System.Linq.Expressions;

namespace MongoDB.Driver;

/// <summary>
/// MongoDB 拓展方法
/// </summary>
public static partial class IAsyncCursorSourceExtensions
{
    /// <summary>
    /// 查找第一个
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <typeparam name="TNewProjection"></typeparam>
    /// <param name="entities"></param>
    /// <param name="projection"></param>
    /// <returns></returns>
    public static TNewProjection FirstOrDefault<TDocument, TNewProjection>(this IFindFluent<TDocument, TDocument> entities, Expression<Func<TDocument, TNewProjection>> projection)
    {
        return entities.Project(projection).FirstOrDefault();
    }

    /// <summary>
    /// 查找第一个
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <typeparam name="TNewProjection"></typeparam>
    /// <param name="entities"></param>
    /// <param name="projection"></param>
    /// <returns></returns>
    public static async Task<TNewProjection> FirstOrDefaultAsync<TDocument, TNewProjection>(this IFindFluent<TDocument, TDocument> entities, Expression<Func<TDocument, TNewProjection>> projection)
    {
        return await entities.Project(projection).FirstOrDefaultAsync();
    }

    /// <summary>
    /// ToList
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <typeparam name="TNewProjection"></typeparam>
    /// <param name="entities"></param>
    /// <param name="projection"></param>
    /// <returns></returns>
    public static List<TNewProjection> ToList<TDocument, TNewProjection>(this IFindFluent<TDocument, TDocument> entities, Expression<Func<TDocument, TNewProjection>> projection)
    {
        return entities.Project(projection).ToList();
    }

    /// <summary>
    /// ToListAsync
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <typeparam name="TNewProjection"></typeparam>
    /// <param name="entities"></param>
    /// <param name="projection"></param>
    /// <returns></returns>
    public static async Task<List<TNewProjection>> ToListAsync<TDocument, TNewProjection>(this IFindFluent<TDocument, TDocument> entities, Expression<Func<TDocument, TNewProjection>> projection)
    {
        return await entities.Project(projection).ToListAsync();
    }

    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <param name="entities"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static MongoDBPagedList<TDocument> ToPagedList<TDocument>(this IFindFluent<TDocument, TDocument> entities, int pageIndex = 1, int pageSize = 20)
    {
        return entities.ToPagedList(a => a, pageIndex, pageSize);
    }

    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <typeparam name="TNewProjection"></typeparam>
    /// <param name="entities"></param>
    /// <param name="projection"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static MongoDBPagedList<TNewProjection> ToPagedList<TDocument, TNewProjection>(this IFindFluent<TDocument, TDocument> entities, Expression<Func<TDocument, TNewProjection>> projection, int pageIndex = 1, int pageSize = 20)
    {
        if (pageIndex <= 0) throw new InvalidOperationException($"{nameof(pageIndex)} must be a positive integer greater than 0.");

        var totalCount = entities.CountDocuments();
        var items = entities.Skip((pageIndex - 1) * pageSize).Limit(pageSize).Project(projection).ToEnumerable();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new MongoDBPagedList<TNewProjection>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            TotalCount = (int)totalCount,
            TotalPages = totalPages,
            HasNextPages = pageIndex < totalPages,
            HasPrevPages = pageIndex - 1 > 0
        };
    }

    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <param name="entities"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<MongoDBPagedList<TDocument>> ToPagedListAsync<TDocument>(this IFindFluent<TDocument, TDocument> entities, int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default)
    {
        return await entities.ToPagedListAsync(a => a, pageIndex, pageSize, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <typeparam name="TNewProjection"></typeparam>
    /// <param name="entities"></param>
    /// <param name="projection"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<MongoDBPagedList<TNewProjection>> ToPagedListAsync<TDocument, TNewProjection>(this IFindFluent<TDocument, TDocument> entities, Expression<Func<TDocument, TNewProjection>> projection, int pageIndex = 1, int pageSize = 20, CancellationToken cancellationToken = default)
    {
        if (pageIndex <= 0) throw new InvalidOperationException($"{nameof(pageIndex)} must be a positive integer greater than 0.");

        var totalCount = await entities.CountDocumentsAsync(cancellationToken);
        var items = entities.Skip((pageIndex - 1) * pageSize).Limit(pageSize).Project(projection).ToEnumerable(cancellationToken: cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new MongoDBPagedList<TNewProjection>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            TotalCount = (int)totalCount,
            TotalPages = totalPages,
            HasNextPages = pageIndex < totalPages,
            HasPrevPages = pageIndex - 1 > 0
        };
    }
}