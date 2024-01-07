// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Encodings.Web;

namespace Furion.Components;

/// <summary>
/// Serve 组件应用服务组件
/// </summary>
[SuppressSniffer]
public sealed class ServeServiceComponent : IServiceComponent
{
    /// <summary>
    /// 装载服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="componentContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Load(IServiceCollection services, ComponentContext componentContext)
    {
        // 控制台日志美化
        services.AddConsoleFormatter();

        // 配置跨域
        services.AddCorsAccessor();

        // 控制器和规范化结果
        services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                })
                .AddInjectWithUnifyResult();
    }
}

/// <summary>
/// Serve 组件应用中间件组件
/// </summary>
[SuppressSniffer]
public sealed class ServeApplicationComponent : IApplicationComponent
{
    /// <summary>
    /// 装载中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="componentContext"></param>
    public void Load(IApplicationBuilder app, IWebHostEnvironment env, ComponentContext componentContext)
    {
        // 配置错误页
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // 401，403 规范化结果
        app.UseUnifyResultStatusCodes();

        // 配置静态
        app.UseStaticFiles();

        // 注册定时任务 UI
        app.UseScheduleUI();

        // 配置路由
        app.UseRouting();

        // 配置跨域
        app.UseCorsAccessor();

        // 配置授权
        app.UseAuthentication();
        app.UseAuthorization();

        // 框架基础配置
        app.UseInject(string.Empty);

        // 配置路由
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}