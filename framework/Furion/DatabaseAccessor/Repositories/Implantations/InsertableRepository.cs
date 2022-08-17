// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 可插入仓储分部类
/// </summary>
public partial class PrivateRepository<TEntity>
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>代理的实体</returns>
    public virtual EntityEntry<TEntity> Insert(TEntity entity, bool? ignoreNullValues = null)
    {
        var entryEntity = Entities.Add(entity);

        // 忽略空值
        IgnoreNullValues(ref entity, ignoreNullValues);

        return entryEntity;
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    public virtual void Insert(params TEntity[] entities)
    {
        Entities.AddRange(entities);
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    public virtual void Insert(IEnumerable<TEntity> entities)
    {
        Entities.AddRange(entities);
    }

    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理的实体</returns>
    public virtual async Task<EntityEntry<TEntity>> InsertAsync(TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        var entityEntry = await Entities.AddAsync(entity, cancellationToken);

        // 忽略空值
        IgnoreNullValues(ref entity, ignoreNullValues);

        return entityEntry;
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    public virtual Task InsertAsync(params TEntity[] entities)
    {
        return Entities.AddRangeAsync(entities);
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns></returns>
    public virtual Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        return Entities.AddRangeAsync(entities, cancellationToken);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中返回的实体</returns>
    public virtual EntityEntry<TEntity> InsertNow(TEntity entity, bool? ignoreNullValues = null)
    {
        var entityEntry = Insert(entity, ignoreNullValues);
        SaveNow();
        return entityEntry;
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <returns>数据库中返回的实体</returns>
    public virtual EntityEntry<TEntity> InsertNow(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null)
    {
        var entityEntry = Insert(entity, ignoreNullValues);
        SaveNow(acceptAllChangesOnSuccess);
        return entityEntry;
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    public virtual void InsertNow(params TEntity[] entities)
    {
        Insert(entities);
        SaveNow();
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    public virtual void InsertNow(TEntity[] entities, bool acceptAllChangesOnSuccess)
    {
        Insert(entities);
        SaveNow(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    public virtual void InsertNow(IEnumerable<TEntity> entities)
    {
        Insert(entities);
        SaveNow();
    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    public virtual void InsertNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess)
    {
        Insert(entities);
        SaveNow(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中返回的实体</returns>
    public virtual async Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        var entityEntry = await InsertAsync(entity, ignoreNullValues, cancellationToken);
        await SaveNowAsync(cancellationToken);
        return entityEntry;
    }

    /// <summary>
    /// 新增一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="ignoreNullValues"></param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>数据库中返回的实体</returns>
    public virtual async Task<EntityEntry<TEntity>> InsertNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, bool? ignoreNullValues = null, CancellationToken cancellationToken = default)
    {
        var entityEntry = await InsertAsync(entity, ignoreNullValues, cancellationToken);
        await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
        return entityEntry;
    }

    /// <summary>
    /// 新增多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    public virtual async Task InsertNowAsync(params TEntity[] entities)
    {
        await InsertAsync(entities);
        await SaveNowAsync();
    }

    /// <summary>
    /// 新增多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    public virtual async Task InsertNowAsync(TEntity[] entities, CancellationToken cancellationToken = default)
    {
        await InsertAsync(entities);
        await SaveNowAsync(cancellationToken);
    }

    /// <summary>
    /// 新增多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    public virtual async Task InsertNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        await InsertAsync(entities);
        await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// 新增多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    public virtual async Task InsertNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await InsertAsync(entities, cancellationToken);
        await SaveNowAsync(cancellationToken);
    }

    /// <summary>
    /// 新增多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    public virtual async Task InsertNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        await InsertAsync(entities, cancellationToken);
        await SaveNowAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}