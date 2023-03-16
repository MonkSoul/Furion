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

namespace Furion.DatabaseAccessor;

/// <summary>
/// 可删除仓储分部类
/// </summary>
public partial class PrivateRepository<TEntity>
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    public virtual EntityEntry<TEntity> Delete(TEntity entity)
    {
        return Entities.Remove(entity);
    }

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    public virtual void Delete(params TEntity[] entities)
    {
        Entities.RemoveRange(entities);
    }

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        Entities.RemoveRange(entities);
    }

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    public virtual Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity)
    {
        return Task.FromResult(Delete(entity));
    }

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    public virtual Task DeleteAsync(params TEntity[] entities)
    {
        Delete(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
    {
        Delete(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    public virtual EntityEntry<TEntity> DeleteNow(TEntity entity)
    {
        var entityEntry = Delete(entity);
        SaveNow();
        return entityEntry;
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <returns></returns>
    public virtual EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess)
    {
        var entityEntry = Delete(entity);
        SaveNow(acceptAllChangesOnSuccess);
        return entityEntry;
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    public virtual void DeleteNow(params TEntity[] entities)
    {
        Delete(entities);
        SaveNow();
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    public virtual void DeleteNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
    {
        Delete(entities);
        SaveNow(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    public virtual void DeleteNow(IEnumerable<TEntity> entities)
    {
        Delete(entities);
        SaveNow();
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    public virtual void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
    {
        Delete(entities);
        SaveNow(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    public virtual async Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await DeleteAsync(entity);
        await SaveNowAsync(cancellationToken);
        return entityEntry;
    }

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    public virtual async Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entityEntry = await DeleteAsync(entity);
        await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        return entityEntry;
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    public virtual async Task DeleteNowAsync(params TEntity[] entities)
    {
        await DeleteAsync(entities);
        await SaveNowAsync();
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    public virtual async Task DeleteNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(entities);
        await SaveNowAsync(cancellationToken);
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    public virtual async Task DeleteNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(entities);
        await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    public virtual async Task DeleteNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(entities);
        await SaveNowAsync(cancellationToken);
    }

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    public virtual async Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(entities);
        await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// 根据主键删除一条记录
    /// </summary>
    /// <param name="key">主键</param>
    public virtual void Delete(object key)
    {
        var deletedEntity = BuildDeletedEntity(key);
        if (deletedEntity != null) return;

        // 如果主键不存在，则采用 Find 查询
        var entity = FindOrDefault(key);
        if (entity != null) Delete(entity);
    }

    /// <summary>
    /// 根据主键删除一条记录
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task</returns>
    public virtual async Task DeleteAsync(object key, CancellationToken cancellationToken = default)
    {
        var deletedEntity = BuildDeletedEntity(key);
        if (deletedEntity != null) return;

        // 如果主键不存在，则采用 FindAsync 查询
        var entity = await FindOrDefaultAsync(key, cancellationToken);
        if (entity != null) await DeleteAsync(entity);
    }

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    public virtual void DeleteNow(object key)
    {
        Delete(key);
        SaveNow();
    }

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    public virtual void DeleteNow(object key, bool acceptAllChangesOnSuccess)
    {
        Delete(key);
        SaveNow(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns></returns>
    public virtual async Task DeleteNowAsync(object key, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(key, cancellationToken);
        await SaveNowAsync(cancellationToken);
    }

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns></returns>
    public virtual async Task DeleteNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(key, cancellationToken);
        await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// 构建被删除的实体
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="isRealDelete">是否真删除</param>
    /// <returns></returns>
    private TEntity BuildDeletedEntity(object key, bool isRealDelete = true)
    {
        // 读取主键
        var keyProperty = EntityType.FindPrimaryKey().Properties.AsEnumerable().FirstOrDefault()?.PropertyInfo;
        if (keyProperty == null) return default;

        // 判断当前主键是否被跟踪了
        var tracking = CheckTrackState(key, out var entityEntry, keyProperty.Name);
        if (tracking)
        {
            // 设置实体状态为已删除
            if (isRealDelete) ChangeEntityState(entityEntry, EntityState.Deleted);

            return entityEntry.Entity as TEntity;
        }

        // 如果没有被跟踪，创建实体对象并设置主键值
        var entity = Activator.CreateInstance<TEntity>();
        keyProperty.SetValue(entity, key);

        // 设置实体状态为已删除
        if (isRealDelete) ChangeEntityState(entity, EntityState.Deleted);

        return entity;
    }
}