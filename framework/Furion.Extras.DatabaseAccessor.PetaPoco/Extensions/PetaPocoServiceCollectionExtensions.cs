// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.6
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using PetaPoco;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// PetaPoco 拓展类
    /// </summary>
    public static class PetaPocoServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 PetaPoco 拓展
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddPetaPoco(this IServiceCollection services, Action<IDatabaseBuildConfiguration> optionsAction)
        {
            services.AddTransient<IDatabase>(sp =>
            {
                var builder = DatabaseConfiguration.Build();
                optionsAction(builder);
                return new Database(builder);
            });
            return services;
        }
    }
}