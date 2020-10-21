// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.20
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur;
using Fur.DependencyInjection;
using Mapster;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 对象映射拓展类
    /// </summary>
    [SkipScan]
    public static class ObjectMapperServiceCollectionExtensions
    {
        /// <summary>
        /// 添加对象映射
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns></returns>
        public static IServiceCollection AddObjectMapper(this IServiceCollection services)
        {
            // 扫描所有继承  IRegister 接口的对象映射配置
            TypeAdapterConfig.GlobalSettings.Scan(App.Assemblies.ToArray());

            TypeAdapterConfig.GlobalSettings.Default
                .NameMatchingStrategy(NameMatchingStrategy.Flexible)
                .PreserveReference(true);

            return services;
        }
    }
}