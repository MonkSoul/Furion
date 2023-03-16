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