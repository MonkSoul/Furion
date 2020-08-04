using Fur.Options;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 选项服务集合拓展类
    /// </summary>
    /// <remarks>
    /// <para>默认启用了选项验证</para>
    /// </remarks>

    public static class FurOptionsServiceCollectionExtensions
    {
        /// <summary>
        /// 添加选项配置
        /// </summary>
        /// <typeparam name="TOptions">继承 <see cref="IFurOptions"/> 的选项类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="callback">选项配置成功后回调</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddFurOptions<TOptions>(this IServiceCollection services, Action<IFurOptions> callback)
            where TOptions : class, IFurOptions
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var optionType = typeof(TOptions);

            var settings = configuration.GetSection(optionType.Name);
            services.AddOptions<TOptions>()
                .Bind(settings)
                .ValidateDataAnnotations();

            var optionInstance = settings.Get<AppOptions>();
            callback?.Invoke(optionInstance);

            return services;
        }
    }
}