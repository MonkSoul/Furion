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

using Furion.SensitiveDetection;
using Microsoft.AspNetCore.Mvc;

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
    /// <returns></returns>
    public static IMvcBuilder AddSensitiveDetection(this IMvcBuilder mvcBuilder)
    {
        var services = mvcBuilder.Services;
        services.AddSensitiveDetection();

        return mvcBuilder;
    }

    /// <summary>
    /// 添加脱敏词汇服务
    /// </summary>
    /// <typeparam name="TSensitiveDetectionProvider"></typeparam>
    /// <param name="mvcBuilder"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public static IMvcBuilder AddSensitiveDetection<TSensitiveDetectionProvider>(this IMvcBuilder mvcBuilder, Action<IServiceCollection> handle = default)
        where TSensitiveDetectionProvider : class, ISensitiveDetectionProvider
    {
        var services = mvcBuilder.Services;

        // 注册脱敏词汇服务
        services.AddSensitiveDetection<TSensitiveDetectionProvider>(handle);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加脱敏词汇服务
    /// <para>需要在入口程序集目录下创建 sensitive-words.txt</para>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSensitiveDetection(this IServiceCollection services)
    {
        return services.AddSensitiveDetection<SensitiveDetectionProvider>();
    }

    /// <summary>
    /// 添加脱敏词汇服务
    /// </summary>
    /// <typeparam name="TSensitiveDetectionProvider"></typeparam>
    /// <param name="services"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public static IServiceCollection AddSensitiveDetection<TSensitiveDetectionProvider>(this IServiceCollection services, Action<IServiceCollection> handle = default)
        where TSensitiveDetectionProvider : class, ISensitiveDetectionProvider
    {
        // 注册脱敏词汇服务
        services.AddSingleton<ISensitiveDetectionProvider, TSensitiveDetectionProvider>();

        // 配置 Mvc 选项
        services.Configure<MvcOptions>(options =>
        {
            // 添加模型绑定器
            options.ModelBinderProviders.Insert(0, new SensitiveDetectionBinderProvider());
        });

        // 自定义配置
        handle?.Invoke(services);

        return services;
    }
}