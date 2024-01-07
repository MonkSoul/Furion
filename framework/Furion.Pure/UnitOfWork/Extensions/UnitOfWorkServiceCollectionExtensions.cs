// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.DatabaseAccessor;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 工作单元拓展类
/// </summary>
[SuppressSniffer]
public static class UnitOfWorkServiceCollectionExtensions
{
    /// <summary>
    /// 添加工作单元服务
    /// </summary>
    /// <param name="mvcBuilder">Mvc构建器</param>
    /// <returns>Mvc构建器</returns>
    public static IMvcBuilder AddUnitOfWork<TUnitOfWork>(this IMvcBuilder mvcBuilder)
        where TUnitOfWork : class, IUnitOfWork
    {
        mvcBuilder.Services.AddUnitOfWork<TUnitOfWork>();

        return mvcBuilder;
    }

    /// <summary>
    /// 添加工作单元服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns>Mvc构建器</returns>
    public static IServiceCollection AddUnitOfWork<TUnitOfWork>(this IServiceCollection services)
        where TUnitOfWork : class, IUnitOfWork
    {
        // 注册工作单元服务
        services.AddTransient<IUnitOfWork, TUnitOfWork>();
        return services;
    }
}