// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Fur
{
    /// <summary>
    /// 应用启动时自动注册中间件
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/startup?view=aspnetcore-3.1#add-configuration-at-startup-from-an-external-assembly
    /// </remarks>
    [SkipScan]
    public class StartupFilter : IStartupFilter
    {
        /// <summary>
        /// dotnet 框架响应报文头
        /// </summary>
        private const string DotNetFrameworkResponseHeader = "dotnet-framework";

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                var applicationServices = app.ApplicationServices;

                // 设置应用服务提供器
                App.ApplicationServices = applicationServices;

                // 设置响应报文头信息，标记框架类型
                app.Use(async (context, next) =>
                {
                    context.Response.Headers[DotNetFrameworkResponseHeader] = nameof(Fur);
                    await next.Invoke();
                });

                // 调用默认中间件
                app.UseApp();

                // 获取环境和配置
                //var env = applicationServices.GetRequiredService<IWebHostEnvironment>();
                //var config = applicationServices.GetRequiredService<IConfiguration>();

                // 调用 Fur.Web.Entry 中的 Startup
                next(app);
            };
        }
    }
}