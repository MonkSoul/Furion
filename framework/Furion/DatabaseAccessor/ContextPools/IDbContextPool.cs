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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库上下文池
/// </summary>
public interface IDbContextPool
{
    /// <summary>
    /// 数据库上下文事务
    /// </summary>
    IDbContextTransaction DbContextTransaction { get; }

    /// <summary>
    /// 获取所有数据库上下文
    /// </summary>
    /// <returns></returns>
    ConcurrentDictionary<Guid, DbContext> GetDbContexts();

    /// <summary>
    /// 保存数据库上下文
    /// </summary>
    /// <param name="dbContext"></param>
    void AddToPool(DbContext dbContext);

    /// <summary>
    /// 保存数据库上下文（异步）
    /// </summary>
    /// <param name="dbContext"></param>
    Task AddToPoolAsync(DbContext dbContext);

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
    /// 保存数据库上下文池中所有已更改的数据库上下文（异步）
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    /// <summary>
    /// 打开事务
    /// </summary>
    /// <param name="ensureTransaction"></param>
    /// <returns></returns>
    void BeginTransaction(bool ensureTransaction = false);

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="withCloseAll">是否自动关闭所有连接</param>
    void CommitTransaction(bool withCloseAll = false);

    /// <summary>
    /// 回滚事务
    /// </summary>
    /// <param name="withCloseAll">是否自动关闭所有连接</param>
    void RollbackTransaction(bool withCloseAll = false);

    /// <summary>
    /// 关闭所有数据库链接
    /// </summary>
    void CloseAll();
}