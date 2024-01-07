// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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
    public void BeginTransaction(FilterContext context, UnitOfWorkAttribute unitOfWork)
    {
        // 开启事务，如果已经开启分布式事务，则无需创建本地事务
        _dbContextPool.BeginTransaction(unitOfWork.EnsureTransaction);
    }

    /// <summary>
    /// 提交工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void CommitTransaction(FilterContext resultContext, UnitOfWorkAttribute unitOfWork)
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

    public void RollbackTransaction(FilterContext resultContext, UnitOfWorkAttribute unitOfWork)
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
    public void OnCompleted(FilterContext context, FilterContext resultContext)
    {
        // 手动关闭
        _dbContextPool.CloseAll();
    }
}