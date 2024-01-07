// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.ViewEngine;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 视图引擎服务拓展类
/// </summary>
[SuppressSniffer]
public static class ViewEngineServiceCollectionExtensions
{
    /// <summary>
    /// 添加视图引擎
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddViewEngine(this IServiceCollection services)
    {
        services.AddTransient<IViewEngine, ViewEngine>();
        return services;
    }
}