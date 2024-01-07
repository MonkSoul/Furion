// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace Furion.DependencyInjection;

/// <summary>
/// 创建作用域静态类
/// </summary>
public static partial class Scoped
{
    /// <summary>
    /// 创建一个工作单元作用域
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="scopeFactory"></param>
    public static void CreateUow(Action<IServiceScopeFactory, IServiceScope> handler, IServiceScopeFactory scopeFactory = default)
    {
        CreateUowAsync(async (fac, scope) =>
        {
            handler(fac, scope);
            await Task.CompletedTask;
        }, scopeFactory).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 创建一个工作单元作用域（异步）
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="scopeFactory"></param>
    public static async Task CreateUowAsync(Func<IServiceScopeFactory, IServiceScope, Task> handler, IServiceScopeFactory scopeFactory = default)
    {
        // 禁止空调用
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        // 创建作用域
        var (scoped, serviceProvider) = CreateScope(ref scopeFactory);

        IDbContextPool dbContextPool = null;

        try
        {
            // 创建一个数据库上下文池
            dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();

            // 开启事务
            dbContextPool.BeginTransaction(true);

            // 执行方法
            await handler(scopeFactory, scoped);

            // 提交工作单元
            dbContextPool.SavePoolNow();

            // 提交事务
            dbContextPool.CommitTransaction(true);
        }
        catch
        {
            // 回滚事务
            dbContextPool?.RollbackTransaction(true);

            throw;
        }
        finally
        {
            // 释放
            scoped.Dispose();
            if (serviceProvider != null) await serviceProvider.DisposeAsync();
        }
    }
}