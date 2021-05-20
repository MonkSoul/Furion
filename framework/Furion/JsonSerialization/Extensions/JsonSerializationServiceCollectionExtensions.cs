// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.6.2
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.JsonSerialization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Json 序列化服务拓展类
    /// </summary>
    [SkipScan]
    public static class JsonSerializationServiceCollectionExtensions
    {
        /// <summary>
        /// 配置 Json 序列化提供器
        /// </summary>
        /// <typeparam name="TJsonSerializerProvider"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddJsonSerialization<TJsonSerializerProvider>(this IServiceCollection services)
            where TJsonSerializerProvider : class, IJsonSerializerProvider
        {
            services.AddSingleton<IJsonSerializerProvider, TJsonSerializerProvider>();
            return services;
        }
    }
}