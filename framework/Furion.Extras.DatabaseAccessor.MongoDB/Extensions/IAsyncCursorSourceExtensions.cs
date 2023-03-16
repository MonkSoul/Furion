// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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