// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.HttpRemote;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     HTTP 远程请求模块 <see cref="IServiceCollection" /> 拓展类
/// </summary>
public static class HttpRemoteServiceCollectionExtensions
{
    /// <summary>
    ///     添加 HTTP 远程请求服务
    /// </summary>
    /// <param name="services">
    ///     <see cref="IServiceCollection" />
    /// </param>
    /// <param name="configure">自定义配置委托</param>
    /// <returns>
    ///     <see cref="IServiceCollection" />
    /// </returns>
    public static IServiceCollection AddHttpRemote(this IServiceCollection services
        , Action<HttpRemoteBuilder>? configure = null)
    {
        // 初始化 HTTP 远程请求构建器
        var httpRemoteBuilder = new HttpRemoteBuilder();

        // 调用自定义配置委托
        configure?.Invoke(httpRemoteBuilder);

        return services.AddHttpRemote(httpRemoteBuilder);
    }

    /// <summary>
    ///     添加 HTTP 远程请求服务
    /// </summary>
    /// <param name="services">
    ///     <see cref="IServiceCollection" />
    /// </param>
    /// <param name="httpRemoteBuilder">
    ///     <see cref="HttpRemoteBuilder" />
    /// </param>
    /// <returns>
    ///     <see cref="IServiceCollection" />
    /// </returns>
    public static IServiceCollection AddHttpRemote(this IServiceCollection services,
        HttpRemoteBuilder httpRemoteBuilder)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(httpRemoteBuilder);

        // 构建模块服务
        httpRemoteBuilder.Build(services);

        return services;
    }
}