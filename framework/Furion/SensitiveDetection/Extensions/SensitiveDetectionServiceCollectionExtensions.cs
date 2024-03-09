// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.SensitiveDetection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 脱敏词汇处理服务
/// </summary>
[SuppressSniffer]
public static class SensitiveDetectionServiceCollectionExtensions
{
    /// <summary>
    /// 添加脱敏词汇服务
    /// <para>需要在入口程序集目录下创建 sensitive-words.txt</para>
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <param name="configureOptionsBuilder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddSensitiveDetection(this IMvcBuilder mvcBuilder, Action<SensitiveDetectionBuilder> configureOptionsBuilder = default)
    {
        var services = mvcBuilder.Services;
        services.AddSensitiveDetection(configureOptionsBuilder);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加脱敏词汇服务
    /// </summary>
    /// <typeparam name="TSensitiveDetectionProvider"></typeparam>
    /// <param name="mvcBuilder"></param>
    /// <param name="configureOptionsBuilder"></param>
    /// <returns></returns>
    public static IMvcBuilder AddSensitiveDetection<TSensitiveDetectionProvider>(this IMvcBuilder mvcBuilder, Action<SensitiveDetectionBuilder> configureOptionsBuilder = default)
        where TSensitiveDetectionProvider : class, ISensitiveDetectionProvider
    {
        var services = mvcBuilder.Services;

        // 注册脱敏词汇服务
        services.AddSensitiveDetection<TSensitiveDetectionProvider>(configureOptionsBuilder);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加脱敏词汇服务
    /// <para>需要在入口程序集目录下创建 sensitive-words.txt</para>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptionsBuilder">configureOptionsBuilder</param>
    /// <returns></returns>
    public static IServiceCollection AddSensitiveDetection(this IServiceCollection services, Action<SensitiveDetectionBuilder> configureOptionsBuilder = default)
    {
        return services.AddSensitiveDetection<SensitiveDetectionProvider>(configureOptionsBuilder);
    }

    /// <summary>
    /// 添加脱敏词汇服务
    /// </summary>
    /// <typeparam name="TSensitiveDetectionProvider"></typeparam>
    /// <param name="services"></param>
    /// <param name="configureOptionsBuilder"></param>
    /// <returns></returns>
    public static IServiceCollection AddSensitiveDetection<TSensitiveDetectionProvider>(this IServiceCollection services, Action<SensitiveDetectionBuilder> configureOptionsBuilder = default)
        where TSensitiveDetectionProvider : class, ISensitiveDetectionProvider
    {
        // 初始化脱敏词汇构建器
        var sensitiveDetectionBuilder = new SensitiveDetectionBuilder();

        // 调用自定义委托
        configureOptionsBuilder?.Invoke(sensitiveDetectionBuilder);

        // 注册脱敏词汇服务
        services.TryAddSingleton<ISensitiveDetectionProvider>(provider =>
        {
            return ActivatorUtilities.CreateInstance<TSensitiveDetectionProvider>(provider, sensitiveDetectionBuilder.EmbedFileName);
        });

        // 配置 Mvc 选项
        services.Configure<MvcOptions>(options =>
        {
            // 添加模型绑定器
            options.ModelBinderProviders.Insert(0, new SensitiveDetectionBinderProvider());
        });

        return services;
    }
}