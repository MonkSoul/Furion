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

        try
        {
            // 创建一个数据库上下文池
            var dbContextPool = scoped.ServiceProvider.GetService<IDbContextPool>();

            // 执行方法
            await handler(scopeFactory, scoped);

            // 提交工作单元
            dbContextPool.SavePoolNow();
        }
        finally
        {
            // 释放
            scoped.Dispose();
            if (serviceProvider != null) await serviceProvider.DisposeAsync();
        }
    }
}