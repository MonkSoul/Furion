// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace System;

/// <summary>
/// 主机静态类
/// </summary>
[SuppressSniffer]
public static class Serve
{
    /// <summary>
    /// 启动默认 Web 主机，含最基础的 Web 注册
    /// </summary>
    /// <param name="urls">默认 5000/5001 端口</param>
    public static void Run(string urls = default)
    {
        Run(RunOptions.Default
            .ConfigureBuilder(builder =>
            {
                // 配置跨域
                builder.Services.AddCorsAccessor();

                // 控制器和规范化结果
                builder.Services.AddControllers()
                        .AddInjectWithUnifyResult();
            })
            .Configure(app =>
            {
                // 配置错误页
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                // 401，403 规范化结果
                app.UseUnifyResultStatusCodes();

                // Https 重定向
                app.UseHttpsRedirection();

                // 配置静态
                app.UseStaticFiles();

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
                app.MapControllers();
            }), urls);
    }

    /// <summary>
    /// 启动泛型 Web 主机
    /// </summary>
    /// <remarks>未包含 Web 基础功能，需手动注册服务/中间件</remarks>
    /// <param name="options">配置选项</param>
    /// <param name="urls">默认 5000/5001 端口</param>
    public static void Run(LegacyRunOptions options, string urls = default)
    {
        var builder = Host.CreateDefaultBuilder(Environment.GetCommandLineArgs());
        options.Builder = builder;

        // 配置 Web 主机
        builder.ConfigureWebHostDefaults(webHostBuilder =>
        {
            webHostBuilder.Inject();
            options.WebHostBuilder = webHostBuilder;

            // 解决部分主机不能正确读取 urls 参数命令问题
            var startUrls = !string.IsNullOrWhiteSpace(urls) ? urls : webHostBuilder.GetSetting("urls");

            // 自定义启动端口
            if (!string.IsNullOrWhiteSpace(startUrls))
            {
                webHostBuilder.UseUrls(startUrls);
            }

            // 调用自定义配置
            options?.ActionWebHostBuilder?.Invoke(webHostBuilder);
        });

        // 调用自定义配置
        options?.ActionBuilder?.Invoke(builder);

        builder.Build().Run();
    }

    /// <summary>
    /// 启动泛型通用主机
    /// </summary>
    /// <param name="options">配置选项</param>
    public static void Run(GenericRunOptions options)
    {
        var builder = Host.CreateDefaultBuilder(Environment.GetCommandLineArgs()).Inject();
        options.Builder = builder;

        // 调用自定义配置
        options?.ActionBuilder?.Invoke(builder);

        builder.Build().Run();
    }

    /// <summary>
    /// 启动 WebApplication 主机
    /// </summary>
    /// <remarks>未包含 Web 基础功能，需手动注册服务/中间件</remarks>
    /// <param name="options">配置选项</param>
    /// <param name="urls">默认 5000/5001 端口</param>
    public static void Run(RunOptions options, string urls = default)
    {
        // 初始化 WebApplicationBuilder
        var builder = (options.Options == null
            ? WebApplication.CreateBuilder(Environment.GetCommandLineArgs())
            : WebApplication.CreateBuilder(options.Options)).Inject();
        options.Builder = builder;

        // 解决部分主机不能正确读取 urls 参数命令问题
        var startUrls = !string.IsNullOrWhiteSpace(urls) ? urls : builder.Configuration["urls"];

        // 自定义启动端口
        if (!string.IsNullOrWhiteSpace(startUrls))
        {
            builder.WebHost.UseUrls(startUrls);
        }

        // 调用自定义配置
        options?.ActionBuilder?.Invoke(builder);

        // 初始化 WebApplication
        var app = builder.Build();
        options.Application = app;

        // 调用自定义配置
        options?.ActionConfigure?.Invoke(app);

        app.Run();
    }
}