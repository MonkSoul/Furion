// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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