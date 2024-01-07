// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.Extensions.DependencyInjection;

namespace Furion.DependencyInjection;

/// <summary>
/// 创建作用域静态类
/// </summary>
[SuppressSniffer]
public static partial class Scoped
{
    /// <summary>
    /// 创建一个作用域范围
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="scopeFactory"></param>
    public static void Create(Action<IServiceScopeFactory, IServiceScope> handler, IServiceScopeFactory scopeFactory = default)
    {
        CreateAsync(async (fac, scope) =>
        {
            handler(fac, scope);
            await Task.CompletedTask;
        }, scopeFactory).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 创建一个作用域范围（异步）
    /// </summary>
    /// <param name="handler"></param>
    /// <param name="scopeFactory"></param>
    public static async Task CreateAsync(Func<IServiceScopeFactory, IServiceScope, Task> handler, IServiceScopeFactory scopeFactory = default)
    {
        // 禁止空调用
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        // 创建作用域
        var (scoped, serviceProvider) = CreateScope(ref scopeFactory);

        try
        {
            // 执行方法
            await handler(scopeFactory, scoped);
        }
        catch
        {
            throw;
        }
        finally
        {
            // 释放
            scoped.Dispose();
            if (serviceProvider != null) await serviceProvider.DisposeAsync();
        }
    }

    /// <summary>
    /// 创建一个作用域
    /// </summary>
    /// <param name="scopeFactory"></param>
    /// <returns></returns>
    private static (IServiceScope Scoped, ServiceProvider ServiceProvider) CreateScope(ref IServiceScopeFactory scopeFactory)
    {
        ServiceProvider undisposeServiceProvider = default;

        if (scopeFactory == null)
        {
            // 默认返回根服务
            if (App.RootServices != null) scopeFactory = App.RootServices.GetService<IServiceScopeFactory>();
            else
            {
                // 这里创建了一个待释放服务提供器（这里会有性能小问题，如果走到这一步）
                undisposeServiceProvider = InternalApp.InternalServices.BuildServiceProvider();
                scopeFactory = undisposeServiceProvider.GetService<IServiceScopeFactory>();
            }
        }

        // 解析服务作用域工厂
        var scoped = scopeFactory.CreateScope();
        return (scoped, undisposeServiceProvider);
    }
}