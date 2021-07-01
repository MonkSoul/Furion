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

using Furion.DataValidation;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 友好异常服务拓展类
    /// </summary>
    [SuppressSniffer]
    public static class DataValidationServiceCollectionExtensions
    {
        /// <summary>
        /// 添加全局数据验证
        /// </summary>
        /// <typeparam name="TValidationMessageTypeProvider">验证类型消息提供器</typeparam>
        /// <param name="mvcBuilder"></param>
        /// <param name="enabledGlobalDataValidationFilter">启用全局验证过滤器</param>
        /// <param name="suppressImplicitRequiredAttributeForNonNullableReferenceTypes">禁止C# 8.0 验证非可空引用类型</param>
        /// <returns></returns>
        public static IMvcBuilder AddDataValidation<TValidationMessageTypeProvider>(this IMvcBuilder mvcBuilder, bool enabledGlobalDataValidationFilter = true, bool suppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            where TValidationMessageTypeProvider : class, IValidationMessageTypeProvider
        {
            var services = mvcBuilder.Services;

            // 添加全局数据验证
            mvcBuilder.AddDataValidation(enabledGlobalDataValidationFilter, suppressImplicitRequiredAttributeForNonNullableReferenceTypes);

            // 单例注册验证消息提供器
            services.AddSingleton<IValidationMessageTypeProvider, TValidationMessageTypeProvider>();

            return mvcBuilder;
        }

        /// <summary>
        /// 添加全局数据验证
        /// </summary>
        /// <typeparam name="TValidationMessageTypeProvider">验证类型消息提供器</typeparam>
        /// <param name="services"></param>
        /// <param name="enabledGlobalDataValidationFilter">启用全局验证过滤器</param>
        /// <param name="suppressImplicitRequiredAttributeForNonNullableReferenceTypes">禁止C# 8.0 验证非可空引用类型</param>
        /// <returns></returns>
        public static IServiceCollection AddDataValidation<TValidationMessageTypeProvider>(this IServiceCollection services, bool enabledGlobalDataValidationFilter = true, bool suppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            where TValidationMessageTypeProvider : class, IValidationMessageTypeProvider
        {
            // 添加全局数据验证
            services.AddDataValidation(enabledGlobalDataValidationFilter, suppressImplicitRequiredAttributeForNonNullableReferenceTypes);

            // 单例注册验证消息提供器
            services.AddSingleton<IValidationMessageTypeProvider, TValidationMessageTypeProvider>();

            return services;
        }

        /// <summary>
        /// 添加全局数据验证
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="enabledGlobalDataValidationFilter">启用全局验证过滤器</param>
        /// <param name="suppressImplicitRequiredAttributeForNonNullableReferenceTypes">禁止C# 8.0 验证非可空引用类型</param>
        /// <returns></returns>
        public static IMvcBuilder AddDataValidation(this IMvcBuilder mvcBuilder, bool enabledGlobalDataValidationFilter = true, bool suppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
        {
            var services = mvcBuilder.Services;

            // 添加验证配置文件支持
            services.AddConfigurableOptions<ValidationTypeMessageSettingsOptions>();

            // 判断是否启用全局
            if (enabledGlobalDataValidationFilter)
            {
                // 关闭引用类型空检查
                mvcBuilder.ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

                // 添加全局数据验证
                mvcBuilder.AddMvcFilter<DataValidationFilter>(options =>
                {
                    // 关闭空引用对象验证
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = suppressImplicitRequiredAttributeForNonNullableReferenceTypes;
                });
            }

            return mvcBuilder;
        }

        /// <summary>
        /// 添加全局数据验证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enabledGlobalDataValidationFilter">启用全局验证过滤器</param>
        /// <param name="suppressImplicitRequiredAttributeForNonNullableReferenceTypes">禁止C# 8.0 验证非可空引用类型</param>
        /// <returns></returns>
        public static IServiceCollection AddDataValidation(this IServiceCollection services, bool enabledGlobalDataValidationFilter = true, bool suppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
        {
            // 添加验证配置文件支持
            services.AddConfigurableOptions<ValidationTypeMessageSettingsOptions>();

            // 判断是否启用全局
            if (enabledGlobalDataValidationFilter)
            {
                // 添加自定义验证
                services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

                // 添加全局数据验证
                services.AddMvcFilter<DataValidationFilter>(options =>
                {
                    // 关闭空引用对象验证
                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = suppressImplicitRequiredAttributeForNonNullableReferenceTypes;
                });
            }

            return services;
        }
    }
}