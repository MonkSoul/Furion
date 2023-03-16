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