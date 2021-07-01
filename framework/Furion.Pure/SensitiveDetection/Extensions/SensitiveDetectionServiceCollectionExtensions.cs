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

using Furion.DependencyInjection;
using Furion.SensitiveDetection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
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

            // 自定义配置
            handle?.Invoke(services);

            return services;
        }
    }
}