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

namespace Furion.DatabaseAccessor;

/// <summary>
/// 实体执行组件
/// </summary>
public sealed partial class EntityExecutePart<TEntity>
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 获取实体同类（族群）
    /// </summary>
    /// <returns>DbSet{TEntity}</returns>
    public DbSet<TEntity> Ethnics()
    {
        return GetRepository().Entities;
    }

    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理的实体</returns>
    public EntityEntry<TEntity> Insert(bool? ignoreNullValues = null)
    {
        return GetRepository().Insert(Entity, ignoreNullValues);
    }

    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>代理的实体</returns>
    public Task<EntityEntry<TEntity>> InsertAsync(bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().InsertAsync(Entity, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> InsertNow(bool? ignoreNullValues = null)
    {
        return GetRepository().InsertNow(Entity, ignoreNullValues);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">接受所有提交更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> InsertNow(bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().InsertNow(Entity, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> InsertNowAsync(bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().InsertNowAsync(Entity, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">接受所有提交更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> InsertNowAsync(bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().InsertNowAsync(Entity, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录
    /// </summary>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> Update(bool? ignoreNullValues = null)
    {
        return GetRepository().Update(Entity, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录
    /// </summary>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateAsync(bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateAsync(Entity, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并立即提交
    /// </summary>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateNow(bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateNow(Entity, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并立即提交
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateNow(bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateNow(Entity, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并立即提交
    /// </summary>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateNowAsync(bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateNowAsync(Entity, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并立即提交
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateNowAsync(bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateNowAsync(Entity, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> UpdateInclude(string[] propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateInclude(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> UpdateInclude(Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateInclude(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> UpdateInclude(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateInclude(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> UpdateInclude(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateInclude(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeAsync(string[] propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeAsync(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeAsync(Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeAsync(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性
    /// </summary>
    /// <param name="propertyNames">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeAsync(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeAsync(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeAsync(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeAsync(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateIncludeNow(string[] propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeNow(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateIncludeNow(string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeNow(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中实体</returns>
    public EntityEntry<TEntity> UpdateIncludeNow(Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeNow(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateIncludeNow(Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeNow(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateIncludeNow(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeNow(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateIncludeNow(IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeNow(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateIncludeNow(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeNow(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateIncludeNow(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateIncludeNow(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(string[] propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateIncludeNowAsync(Entity, propertyNames, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateIncludeNowAsync(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateIncludeNowAsync(Entity, propertyPredicates, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateIncludeNowAsync(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(IEnumerable<string> propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateIncludeNowAsync(Entity, propertyNames, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateIncludeNowAsync(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateIncludeNowAsync(Entity, propertyPredicates, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中的特定属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateIncludeNowAsync(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateIncludeNowAsync(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> UpdateExclude(string[] propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExclude(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> UpdateExclude(Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExclude(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> UpdateExclude(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExclude(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录中特定属性
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> UpdateExclude(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExclude(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeAsync(string[] propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeAsync(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeAsync(Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeAsync(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性
    /// </summary>
    /// <param name="propertyNames">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeAsync(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeAsync(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeAsync(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeAsync(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateExcludeNow(string[] propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeNow(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateExcludeNow(string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeNow(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中实体</returns>
    public EntityEntry<TEntity> UpdateExcludeNow(Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeNow(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateExcludeNow(Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeNow(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateExcludeNow(IEnumerable<string> propertyNames, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeNow(Entity, propertyNames, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateExcludeNow(IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeNow(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateExcludeNow(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeNow(Entity, propertyPredicates, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中的实体</returns>
    public EntityEntry<TEntity> UpdateExcludeNow(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        return GetRepository().UpdateExcludeNow(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(string[] propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateExcludeNowAsync(Entity, propertyNames, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(string[] propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateExcludeNowAsync(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(Expression<Func<TEntity, object>>[] propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateExcludeNowAsync(Entity, propertyPredicates, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(Expression<Func<TEntity, object>>[] propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateExcludeNowAsync(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(IEnumerable<string> propertyNames, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateExcludeNowAsync(Entity, propertyNames, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyNames">属性名</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(IEnumerable<string> propertyNames, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateExcludeNowAsync(Entity, propertyNames, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateExcludeNowAsync(Entity, propertyPredicates, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 更新一条记录并排除属性并立即提交
    /// </summary>
    /// <param name="propertyPredicates">属性表达式</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中的实体</returns>
    public Task<EntityEntry<TEntity>> UpdateExcludeNowAsync(IEnumerable<Expression<Func<TEntity, object>>> propertyPredicates, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        return GetRepository().UpdateExcludeNowAsync(Entity, propertyPredicates, acceptAllChangesOnSuccess, ignoreNullValues, cancellationToken);
    }

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> Delete()
    {
        return GetRepository().Delete(Entity);
    }

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> DeleteAsync()
    {
        return GetRepository().DeleteAsync(Entity);
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <returns>代理中的实体</returns>
    public EntityEntry<TEntity> DeleteNow()
    {
        return GetRepository().DeleteNow(Entity);
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <returns></returns>
    public EntityEntry<TEntity> DeleteNow(bool acceptAllChangesOnSuccess)
    {
        return GetRepository().DeleteNow(Entity, acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> DeleteNowAsync(CancellationToken cancellationToken = default)
    {
        return GetRepository().DeleteNowAsync(Entity, cancellationToken);
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    public Task<EntityEntry<TEntity>> DeleteNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return GetRepository().DeleteNowAsync(Entity, acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// 获取实体仓储
    /// </summary>
    /// <returns></returns>
    private IPrivateRepository<TEntity> GetRepository()
    {
        // 判断是否在非 Web 中执行
        if (ContextScoped == null && App.HttpContext?.RequestServices == null) throw new InvalidOperationException("Entity Extensions：It is detected that it is executed in a non Web environment. Please create the scope and pass `.SetContextScoped(serviceProvider)` incoming.");

        return Db.GetRepository<TEntity>(DbContextLocator, ContextScoped);
    }
}