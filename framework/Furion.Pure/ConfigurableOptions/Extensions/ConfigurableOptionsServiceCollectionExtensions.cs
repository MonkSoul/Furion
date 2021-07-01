// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion;
using Furion.ConfigurableOptions;
using Furion.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 可变选项服务拓展类
    /// </summary>
    [SuppressSniffer]
    public static class ConfigurableOptionsServiceCollectionExtensions
    {
        /// <summary>
        /// 添加选项配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddConfigurableOptions<TOptions>(this IServiceCollection services)
            where TOptions : class, IConfigurableOptions
        {
            var optionsType = typeof(TOptions);
            var optionsSettings = optionsType.GetCustomAttribute<OptionsSettingsAttribute>(false);

            // 获取键名
            var jsonKey = GetOptionsJsonKey(optionsSettings, optionsType);

            // 配置选项（含验证信息）
            var configurationRoot = App.Configuration;
            var optionsConfiguration = configurationRoot.GetSection(jsonKey);

            // 配置选项监听
            if (typeof(IConfigurableOptionsListener<TOptions>).IsAssignableFrom(optionsType))
            {
                var onListenerMethod = optionsType.GetMethod(nameof(IConfigurableOptionsListener<TOptions>.OnListener));
                if (onListenerMethod != null)
                {
                    ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
                    {
                        var options = optionsConfiguration.Get<TOptions>();
                        onListenerMethod.Invoke(options, new object[] { options, optionsConfiguration });
                    });
                }
            }

            services.AddOptions<TOptions>()
                .Bind(optionsConfiguration, options =>
                {
                    options.BindNonPublicProperties = true; // 绑定私有变量
                })
                .ValidateDataAnnotations();

            // 配置复杂验证后后期配置
            var validateInterface = optionsType.GetInterfaces()
                .FirstOrDefault(u => u.IsGenericType && typeof(IConfigurableOptions).IsAssignableFrom(u.GetGenericTypeDefinition()));
            if (validateInterface != null)
            {
                var genericArguments = validateInterface.GenericTypeArguments;

                // 配置复杂验证
                if (genericArguments.Length > 1)
                {
                    services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IValidateOptions<TOptions>), genericArguments.Last()));
                }

                // 配置后期配置
                var postConfigureMethod = optionsType.GetMethod(nameof(IConfigurableOptions<TOptions>.PostConfigure));
                if (postConfigureMethod != null)
                {
                    if (optionsSettings?.PostConfigureAll != true)
                        services.PostConfigure<TOptions>(options => postConfigureMethod.Invoke(options, new object[] { options, optionsConfiguration }));
                    else
                        services.PostConfigureAll<TOptions>(options => postConfigureMethod.Invoke(options, new object[] { options, optionsConfiguration }));
                }
            }

            return services;
        }

        /// <summary>
        /// 获取选项键
        /// </summary>
        /// <param name="optionsSettings">选项配置特性</param>
        /// <param name="optionsType">选项类型</param>
        /// <returns></returns>
        private static string GetOptionsJsonKey(OptionsSettingsAttribute optionsSettings, Type optionsType)
        {
            // 默认后缀
            var defaultStuffx = nameof(Options);

            return optionsSettings switch
            {
                // // 没有贴 [OptionsSettings]，如果选项类以 `Options` 结尾，则移除，否则返回类名称
                null => optionsType.Name.EndsWith(defaultStuffx) ? optionsType.Name[0..^defaultStuffx.Length] : optionsType.Name,
                // 如果贴有 [OptionsSettings] 特性，但未指定 JsonKey 参数，则直接返回类名，否则返回 JsonKey
                _ => optionsSettings != null && string.IsNullOrWhiteSpace(optionsSettings.JsonKey) ? optionsType.Name : optionsSettings.JsonKey,
            };
        }
    }
}