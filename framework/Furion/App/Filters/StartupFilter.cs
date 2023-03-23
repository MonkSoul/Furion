// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Furion;

/// <summary>
/// 应用启动时自动注册中间件
/// </summary>
/// <remarks>
/// </remarks>
[SuppressSniffer]
public class StartupFilter : IStartupFilter
{
    /// <summary>
    /// 配置中间件
    /// </summary>
    /// <param name="next"></param>
    /// <returns></returns>
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            // 存储根服务
            InternalApp.RootServices = app.ApplicationServices;

            // 环境名
            var envName = App.HostEnvironment?.EnvironmentName ?? "Unknown";

            // 设置响应报文头信息
            app.Use(async (context, next) =>
            {
                // 处理 WebSocket 请求
                if (context.IsWebSocketRequest()) await next.Invoke();
                else
                {
                    // 输出当前环境标识
                    context.Response.Headers["environment"] = envName;

                    // 执行下一个中间件
                    await next.Invoke();

                    // 解决刷新 Token 时间和 Token 时间相近问题
                    if (!context.Response.HasStarted
                        && context.Response.StatusCode == StatusCodes.Status401Unauthorized
                        && context.Response.Headers.ContainsKey("access-token")
                        && context.Response.Headers.ContainsKey("x-access-token"))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    }

                    // 释放所有未托管的服务提供器
                    App.DisposeUnmanagedObjects();
                }
            });

            // 调用默认中间件
            app.UseApp();

            // 配置所有 Starup Configure
            UseStartups(app);

            // 调用启动层的 Startup
            next(app);
        };
    }

    /// <summary>
    /// 配置 Startup 的 Configure
    /// </summary>
    /// <param name="app">应用构建器</param>
    private static void UseStartups(IApplicationBuilder app)
    {
        // 反转，处理排序
        var startups = App.AppStartups.Reverse();
        if (!startups.Any()) return;

        // 处理【部署】二级虚拟目录
        var virtualPath = App.Settings.VirtualPath;
        if (!string.IsNullOrWhiteSpace(virtualPath) && virtualPath.StartsWith("/"))
        {
            app.Map(virtualPath, _app => UseStartups(startups, _app));
            return;
        }

        UseStartups(startups, app);
    }

    /// <summary>
    /// 批量将自定义 AppStartup 添加到 Startup.cs 的 Configure 中
    /// </summary>
    /// <param name="startups"></param>
    /// <param name="app"></param>
    private static void UseStartups(IEnumerable<AppStartup> startups, IApplicationBuilder app)
    {
        // 遍历所有
        foreach (var startup in startups)
        {
            var type = startup.GetType();

            // 获取所有符合依赖注入格式的方法，如返回值 void，且第一个参数是 IApplicationBuilder 类型
            var configureMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(u => u.ReturnType == typeof(void)
                    && u.GetParameters().Length > 0
                    && u.GetParameters().First().ParameterType == typeof(IApplicationBuilder));

            if (!configureMethods.Any()) continue;

            // 自动安装属性调用
            foreach (var method in configureMethods)
            {
                method.Invoke(startup, ResolveMethodParameterInstances(app, method));
            }
        }

        // 释放内存
        App.AppStartups.Clear();
    }

    /// <summary>
    /// 解析方法参数实例
    /// </summary>
    /// <param name="app"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    private static object[] ResolveMethodParameterInstances(IApplicationBuilder app, MethodInfo method)
    {
        // 获取方法所有参数
        var parameters = method.GetParameters();
        var parameterInstances = new object[parameters.Length];
        parameterInstances[0] = app;

        // 解析服务
        for (var i = 1; i < parameters.Length; i++)
        {
            var parameter = parameters[i];
            parameterInstances[i] = app.ApplicationServices.GetRequiredService(parameter.ParameterType);
        }

        return parameterInstances;
    }
}