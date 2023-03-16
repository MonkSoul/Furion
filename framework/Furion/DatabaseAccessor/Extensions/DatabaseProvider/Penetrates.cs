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
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 数据库上下文描述器
    /// </summary>
    internal static readonly ConcurrentDictionary<Type, Type> DbContextDescriptors;

    /// <summary>
    /// 构造函数
    /// </summary>
    static Penetrates()
    {
        DbContextDescriptors = new ConcurrentDictionary<Type, Type>();
    }

    /// <summary>
    /// 配置 SqlServer 数据库上下文
    /// </summary>
    /// <param name="optionBuilder">数据库上下文选项构建器</param>
    /// <param name="interceptors">拦截器</param>
    /// <returns></returns>
    internal static Action<IServiceProvider, DbContextOptionsBuilder> ConfigureDbContext(Action<IServiceProvider, DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
    {
        return (serviceProvider, options) =>
        {
            // 只有开发环境开启
            if (App.HostEnvironment?.IsDevelopment() ?? false)
            {
                options/*.UseLazyLoadingProxies()*/
                         .EnableDetailedErrors()
                         .EnableSensitiveDataLogging();
            }

            optionBuilder?.Invoke(serviceProvider, options);

            // 添加拦截器
            AddInterceptors(interceptors, options);
        };
    }

    /// <summary>
    /// 检查数据库上下文是否绑定
    /// </summary>
    /// <param name="dbContextLocatorType"></param>
    /// <param name="dbContextType"></param>
    /// <returns></returns>
    internal static void CheckDbContextLocator(Type dbContextLocatorType, out Type dbContextType)
    {
        if (!DbContextDescriptors.TryGetValue(dbContextLocatorType, out dbContextType)) throw new InvalidCastException($" The dbcontext locator `{dbContextLocatorType.Name}` is not bind.");
    }

    /// <summary>
    /// 数据库数据库拦截器
    /// </summary>
    /// <param name="interceptors">拦截器</param>
    /// <param name="options"></param>
    private static void AddInterceptors(IInterceptor[] interceptors, DbContextOptionsBuilder options)
    {
        // 添加拦截器
        var interceptorList = DbProvider.GetDefaultInterceptors();

        if (interceptors != null || interceptors.Length > 0)
        {
            interceptorList.AddRange(interceptors);
        }
        options.AddInterceptors(interceptorList.ToArray());
    }
}