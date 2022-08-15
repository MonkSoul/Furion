// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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