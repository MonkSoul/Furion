// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion.DataValidation;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

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
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddDataValidation<TValidationMessageTypeProvider>(this IMvcBuilder mvcBuilder, Action<DataValidationServiceOptions> configure = null)
        where TValidationMessageTypeProvider : class, IValidationMessageTypeProvider
    {
        // 添加全局数据验证
        mvcBuilder.Services.AddDataValidation<TValidationMessageTypeProvider>(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加全局数据验证
    /// </summary>
    /// <typeparam name="TValidationMessageTypeProvider">验证类型消息提供器</typeparam>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataValidation<TValidationMessageTypeProvider>(this IServiceCollection services, Action<DataValidationServiceOptions> configure = null)
        where TValidationMessageTypeProvider : class, IValidationMessageTypeProvider
    {
        // 添加全局数据验证
        services.AddDataValidation(configure);

        // 单例注册验证消息提供器
        services.AddSingleton<IValidationMessageTypeProvider, TValidationMessageTypeProvider>();

        return services;
    }

    /// <summary>
    /// 添加全局数据验证
    /// </summary>
    /// <param name="mvcBuilder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMvcBuilder AddDataValidation(this IMvcBuilder mvcBuilder, Action<DataValidationServiceOptions> configure = null)
    {
        mvcBuilder.Services.AddDataValidation(configure);

        return mvcBuilder;
    }

    /// <summary>
    /// 添加全局数据验证
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataValidation(this IServiceCollection services, Action<DataValidationServiceOptions> configure = null)
    {
        // 添加验证配置文件支持
        services.AddConfigurableOptions<ValidationTypeMessageSettingsOptions>();

        // 载入服务配置选项
        var configureOptions = new DataValidationServiceOptions();
        configure?.Invoke(configureOptions);

        // 判断是否启用全局
        if (configureOptions.EnableGlobalDataValidation)
        {
            // 启用了全局验证，则默认关闭原生 ModelStateInvalidFilter 验证
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = configureOptions.SuppressModelStateInvalidFilter;
            });

            // 添加全局数据验证
            services.AddMvcFilter<DataValidationFilter>(options =>
            {
                // 关闭空引用对象验证
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = configureOptions.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes;
            });
        }

        return services;
    }
}