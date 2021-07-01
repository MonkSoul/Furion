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

using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 对象映射拓展类
    /// </summary>
    public static class ObjectMapperServiceCollectionExtensions
    {
        /// <summary>
        /// 添加对象映射
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="assemblies">扫描的程序集</param>
        /// <returns></returns>
        public static IServiceCollection AddObjectMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            // 获取全局映射配置
            var config = TypeAdapterConfig.GlobalSettings;

            // 扫描所有继承  IRegister 接口的对象映射配置
            if (assemblies != null && assemblies.Length > 0) config.Scan(assemblies);

            // 配置默认全局映射（支持覆盖）
            config.Default
                  .NameMatchingStrategy(NameMatchingStrategy.Flexible)
                  .PreserveReference(true);

            // 配置支持依赖注入
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}