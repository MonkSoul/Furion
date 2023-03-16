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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Furion.DatabaseAccessor.Extensions;

/// <summary>
/// 实体多数据库上下文拓展类
/// </summary>
[SuppressSniffer]
public static class IEntityWithDbContextLocatorExtensions
{
    /// <summary>
    /// 获取实体同类（族群）
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <returns>DbSet{TEntity}</returns>
    public static DbSet<TEntity> Ethnics<TEntity, TDbContextLocator>(this TEntity entity)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().Ethnics();
    }

    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理的实体</returns>
    public static EntityEntry<TEntity> Insert<TEntity, TDbContextLocator>(this TEntity entity, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().Insert(ignoreNullValues);
    }

    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>代理的实体</returns>
    public static Task<EntityEntry<TEntity>> InsertAsync<TEntity, TDbContextLocator>(this TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().InsertAsync(ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> InsertNow<TEntity, TDbContextLocator>(this TEntity entity, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().InsertNow(ignoreNullValues);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有提交更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> InsertNow<TEntity, TDbContextLocator>(this TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().InsertNow(acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> InsertNowAsync<TEntity, TDbContextLocator>(this TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().InsertNowAsync(ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有提交更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> InsertNowAsync<TEntity, TDbContextLocator>(this TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().InsertNowAsync(acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> Update<TEntity, TDbContextLocator>(this TEntity entity, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().Update(ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateAsync<TEntity, TDbContextLocator>(this TEntity entity, bool? ignoreNullValues = null)
         where TEntity : class, IPrivateEntity, new()
         where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateAsync(ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateNow<TEntity, TDbContextLocator>(this TEntity entity, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateNow(ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateNow<TEntity, TDbContextLocator>(this TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
         where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateNow(acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateNowAsync<TEntity, TDbContextLocator>(this TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateNowAsync(ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateNowAsync<TEntity, TDbContextLocator>(this TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateNowAsync(acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> UpdateInclude<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateInclude(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> UpdateInclude<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateInclude(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> UpdateInclude<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateInclude(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> UpdateInclude<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateInclude(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeAsync<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeAsync(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeAsync<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeAsync(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeAsync(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeAsync(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateIncludeNow<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNow(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateIncludeNow<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNow(propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中实体</returns>
    public static EntityEntry<TEntity> UpdateIncludeNow<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNow(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateIncludeNow<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNow(propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateIncludeNow<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNow(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateIncludeNow<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
         where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNow(propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateIncludeNow<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNow(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateIncludeNow<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNow(propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNowAsync(propertyNames, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNowAsync(propertyNames, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNowAsync(propertyPredicates, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNowAsync(propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNowAsync(propertyNames, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNowAsync(propertyNames, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNowAsync(propertyPredicates, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateIncludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateIncludeNowAsync(propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> UpdateExclude<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExclude(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> UpdateExclude<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExclude(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> UpdateExclude<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExclude(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> UpdateExclude<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExclude(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeAsync<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeAsync(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeAsync<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeAsync(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeAsync(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeAsync(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateExcludeNow<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNow(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateExcludeNow<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNow(propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中实体</returns>
    public static EntityEntry<TEntity> UpdateExcludeNow<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNow(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateExcludeNow<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNow(propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateExcludeNow<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNow(propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateExcludeNow<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
         where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNow(propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateExcludeNow<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNow(propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public static EntityEntry<TEntity> UpdateExcludeNow<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNow(propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNowAsync(propertyNames, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNowAsync(propertyNames, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNowAsync(propertyPredicates, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNowAsync(propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNowAsync(propertyNames, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNowAsync(propertyNames, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNowAsync(propertyPredicates, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public static Task<EntityEntry<TEntity>> UpdateExcludeNowAsync<TEntity, TDbContextLocator>(this TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().UpdateExcludeNowAsync(propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> Delete<TEntity, TDbContextLocator>(this TEntity entity)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().Delete();
    }

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> DeleteAsync<TEntity, TDbContextLocator>(this TEntity entity)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().DeleteAsync();
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    public static EntityEntry<TEntity> DeleteNow<TEntity, TDbContextLocator>(this TEntity entity)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().DeleteNow();
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <returns></returns>
    public static EntityEntry<TEntity> DeleteNow<TEntity, TDbContextLocator>(this TEntity entity, bool acceptAllChangesOnSuccess)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().DeleteNow(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> DeleteNowAsync<TEntity, TDbContextLocator>(this TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().DeleteNowAsync(cancellationToken);
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    public static Task<EntityEntry<TEntity>> DeleteNowAsync<TEntity, TDbContextLocator>(this TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
        return EntityExecutePart<TEntity>.Default().SetEntity(entity).Change<TDbContextLocator>().DeleteNowAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}