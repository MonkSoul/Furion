using Fur;
using Fur.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 配置选项拓展类
    /// </summary>
    public static class AppServiceCollectionExtensions
    {
        /// <summary>
        /// 添加应用配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">服务配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddApp(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            App.Services = services;
            var serviceProvider = App.ServiceProvider;
            services.AddAppOptions<AppSettingsOptions>();

            configure?.Invoke(services);
            return services;
        }

        /// <summary>
        /// 添加选项配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="options">选项实例</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddAppOptions<TOptions>(this IServiceCollection services)
            where TOptions : class, IAppOptions
        {
            var optionsType = typeof(TOptions);
            string jsonKey = null;

            if (optionsType.IsDefined(typeof(OptionsSettingsAttribute), false))
            {
                var optionsSettings = optionsType.GetCustomAttribute<OptionsSettingsAttribute>(false);
                jsonKey = optionsSettings.JsonKey;
            }

            jsonKey ??= optionsType.Name;

            var optionsConfiguration = App.Configuration.GetSection(jsonKey);
            services.AddOptions<TOptions>()
                .Bind(optionsConfiguration)
                .ValidateDataAnnotations();

            // 配置复杂验证后后期配置
            var validateInterface = optionsType.GetInterfaces()
                .FirstOrDefault(u => u.IsGenericType && typeof(IAppOptions).IsAssignableFrom(u.GetGenericTypeDefinition()));
            if (validateInterface != null)
            {
                var genericArguments = validateInterface.GetGenericArguments();

                // 配置复杂验证
                if (genericArguments.Length > 1)
                {
                    services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IValidateOptions<TOptions>), validateInterface.GetGenericArguments().Last()));
                }

                // 配置后期配置
                var postConfigureMethod = optionsType.GetMethod(nameof(IAppOptions<TOptions>.PostConfigure));
                if (postConfigureMethod != null)
                {
                    services.PostConfigure<TOptions>(options =>
                    {
                        postConfigureMethod.Invoke(options, new object[] { options });
                    });
                }
            }

            return services;
        }
    }
}