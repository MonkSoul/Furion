// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur;
using Fur.CorsAccessor;
using Fur.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 跨域中间件拓展
    /// </summary>
    [SkipScan]
    public static class CorsAccessorApplicationBuilderExtensions
    {
        /// <summary>
        /// 添加跨域中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsAccessor(this IApplicationBuilder app)
        {
            // 获取选项
            var corsAccessorSettings = App.GetOptions<CorsAccessorSettingsOptions>();

            // 配置跨域中间件
            app.UseCors(corsAccessorSettings.PolicyName);

            // 添加压缩缓存
            app.UseResponseCaching();

            return app;
        }
    }
}