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

using Microsoft.AspNetCore.Mvc.Filters;

namespace Furion.DatabaseAccessor;

/// <summary>
/// EFCore 工作单元实现
/// </summary>
[SuppressSniffer]
public sealed class EFCoreUnitOfWork : IUnitOfWork
{
    /// <summary>
    /// 数据库上下文池
    /// </summary>
    private readonly IDbContextPool _dbContextPool;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContextPool"></param>
    public EFCoreUnitOfWork(IDbContextPool dbContextPool)
    {
        _dbContextPool = dbContextPool;
    }

    /// <summary>
    /// 开启工作单元处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="unitOfWork"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void BeginTransaction(ActionExecutingContext context, UnitOfWorkAttribute unitOfWork)
    {
        // 开启事务
        _dbContextPool.BeginTransaction(unitOfWork.EnsureTransaction);
    }

    /// <summary>
    /// 提交工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void CommitTransaction(ActionExecutedContext resultContext, UnitOfWorkAttribute unitOfWork)
    {
        // 提交事务
        _dbContextPool.CommitTransaction();
    }

    /// <summary>
    /// 回滚工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
    /// <exception cref="NotImplementedException"></exception>

    public void RollbackTransaction(ActionExecutedContext resultContext, UnitOfWorkAttribute unitOfWork)
    {
        // 回滚事务
        _dbContextPool.RollbackTransaction();
    }

    /// <summary>
    /// 执行完毕（无论成功失败）
    /// </summary>
    /// <param name="context"></param>
    /// <param name="resultContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnCompleted(ActionExecutingContext context, ActionExecutedContext resultContext)
    {
        // 手动关闭
        _dbContextPool.CloseAll();
    }
}