using Fur.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                // 设置响应报文头信息，标记框架类型
                app.Use(async (context, next) =>
                {
                    context.Response.Headers[DotNetFrameworkResponseHeader] = "Fur";
                    await next.Invoke();
                });

                // 调用默认中间件
                app.UseApp();

                UseStartup(app, app.ApplicationServices);

                // 调用 Fur.Web.Entry 中的 Startup
                next(app);
            };
        }

        /// <summary>
        /// 配置 Startup 的 Configure
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <param name="applicationServices">服务提供器</param>
        private static void UseStartup(IApplicationBuilder app, IServiceProvider applicationServices)
        {
            var startups = App.AppStartups;
            if (!startups.Any()) return;

            // 获取环境和配置
            var env = applicationServices.GetRequiredService<IWebHostEnvironment>();

            foreach (var startup in startups)
            {
                var type = startup.GetType();

                // 获取所有符合依赖注入格式的方法，如返回值void，且第一个参数是 IApplicationBuilder 类型
                var configureMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(u => u.ReturnType == typeof(void)
                        && u.GetParameters().Length > 0
                        && u.GetParameters().First().ParameterType == typeof(IApplicationBuilder));

                if (!configureMethods.Any()) continue;

                // 自动安装属性调用
                foreach (var method in configureMethods)
                {
                    method.Invoke(startup, ResolveMethodParameterInstances(app, applicationServices, method).ToArray());
                }
            }
        }

        /// <summary>
        /// 解析方法参数实例
        /// </summary>
        /// <param name="app"></param>
        /// <param name="applicationServices"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static List<object> ResolveMethodParameterInstances(IApplicationBuilder app, IServiceProvider applicationServices, MethodInfo method)
        {
            var parameterInstances = new List<object>() { app };
            var methodParams = method.GetParameters().Skip(1);
            foreach (var parameterInfo in methodParams)
            {
                parameterInstances.Add(applicationServices.GetRequiredService(parameterInfo.ParameterType));
            }
            return parameterInstances;
        }
    }
}