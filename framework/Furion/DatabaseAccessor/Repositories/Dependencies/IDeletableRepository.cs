// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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
/// 可删除仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public partial interface IDeletableRepository<TEntity> : IDeletableRepository<TEntity, MasterDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
{
}

/// <summary>
/// 可删除仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
public partial interface IDeletableRepository<TEntity, TDbContextLocator> : IPrivateDeletableRepository<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{
}

/// <summary>
/// 可删除仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IPrivateDeletableRepository<TEntity> : IPrivateRootRepository
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    EntityEntry<TEntity> Delete(TEntity entity);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    void Delete(params TEntity[] entities);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    void Delete(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    Task DeleteAsync(params TEntity[] entities);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    Task DeleteAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>代理中的实体</returns>
    EntityEntry<TEntity> DeleteNow(TEntity entity);

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <returns></returns>
    EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    void DeleteNow(params TEntity[] entities);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    void DeleteNow(TEntity[] entities, bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    void DeleteNow(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除一条记录并立即提交
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>代理中的实体</returns>
    Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(params TEntity[] entities);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(TEntity[] entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除多条记录并立即提交
    /// </summary>
    /// <param name="entities">多个实体</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">取消异步令牌</param>
    /// <returns>Task</returns>
    Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据主键删除一条记录
    /// </summary>
    /// <param name="key">主键</param>
    void Delete(object key);

    /// <summary>
    /// 根据主键删除一条记录
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task</returns>
    Task DeleteAsync(object key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    void DeleteNow(object key);

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    void DeleteNow(object key, bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns></returns>
    Task DeleteNowAsync(object key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据主键删除一条记录并立即提交
    /// </summary>
    /// <param name="key">主键</param>
    /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns></returns>
    Task DeleteNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
}