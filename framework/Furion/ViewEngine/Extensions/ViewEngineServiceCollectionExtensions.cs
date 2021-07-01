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
using Furion.ViewEngine;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 视图引擎服务拓展类
    /// </summary>
    [SuppressSniffer]
    public static class ViewEngineServiceCollectionExtensions
    {
        /// <summary>
        /// 添加视图引擎
        /// </summary>
        /// <param name="services"></param>
        /// <param name="templateSaveDir"></param>
        /// <returns></returns>
        public static IServiceCollection AddViewEngine(this IServiceCollection services, string templateSaveDir = default)
        {
            if (!string.IsNullOrWhiteSpace(templateSaveDir)) Penetrates.TemplateSaveDir = templateSaveDir;

            services.AddTransient<IViewEngine, ViewEngine>();
            return services;
        }
    }
}