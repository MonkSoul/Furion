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

namespace Furion.DatabaseAccessor;

/// <summary>
/// 可写仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public partial interface IWritableRepository<TEntity> : IWritableRepository<TEntity, MasterDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
{
}

/// <summary>
/// 可写仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
public partial interface IWritableRepository<TEntity, TDbContextLocator> : IPrivateWritableRepository<TEntity>
    , IInsertableRepository<TEntity>
    , IUpdateableRepository<TEntity>
    , IDeletableRepository<TEntity>
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{
}

/// <summary>
/// 可写仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public partial interface IPrivateWritableRepository<TEntity>
    : IPrivateInsertableRepository<TEntity>
    , IPrivateUpdateableRepository<TEntity>
    , IPrivateDeletableRepository<TEntity>
    , IPrivateRootRepository
    where TEntity : class, IPrivateEntity, new()
{
    /// <summary>
    /// 接受所有更改
    /// </summary>
    void AcceptAllChanges();

    /// <summary>
    /// 保存数据库上下文池中所有已更改的数据库上下文
    /// </summary>
    /// <returns></returns>
    int SavePoolNow();

    /// <summary>
    /// 保存数据库上下文池中所有已更改的数据库上下文
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <returns></returns>
    int SavePoolNow(bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 保存数据库上下文池中所有已更改的数据库上下文
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 保存数据库上下文池中所有已更改的数据库上下文
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 提交更改操作
    /// </summary>
    /// <returns></returns>
    int SaveNow();

    /// <summary>
    /// 提交更改操作
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <returns></returns>
    int SaveNow(bool acceptAllChangesOnSuccess);

    /// <summary>
    /// 提交更改操作（异步）
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveNowAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 提交更改操作（异步）
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
}