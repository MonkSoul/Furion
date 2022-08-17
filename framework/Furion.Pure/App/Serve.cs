// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion;
using Furion.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace System;

/// <summary>
/// 主机静态类
/// </summary>
[SuppressSniffer]
public static class Serve
{
    /// <summary>
    /// 静默启动排除日志分类名
    /// </summary>
    private static readonly string[] SilenceExcludesOfLogCategoryName = new string[]
    {
        "Microsoft.Hosting"
        , "Microsoft.AspNetCore"
        , "Microsoft.Extensions.Hosting"
    };

    /// <summary>
    /// 启动默认 Web 主机，含最基础的 Web 注册
    /// </summary>
    /// <param name="urls">默认 5000/5001 端口</param>
    /// <param name="silence">静默启动</param>
    /// <param name="logging">静默启动日志状态，默认 false</param>
    /// <param name="args">启动参数</param>
    public static void Run(string urls = default, bool silence = false, bool logging = false, string[] args = default)
    {
#if !NET5_0
        Run(RunOptions.Default
            .WithArgs(args)
            .Silence(silence, logging)
            .AddComponent<ServeServiceComponent>()
            .UseComponent<ServeApplicationComponent>(), urls);
#else
        Run((LegacyRunOptions.Default
            .WithArgs(args)
            .Silence(silence, logging)
            .AddComponent<ServeServiceComponent>())
            .UseComponent<ServeApplicationComponent>(), urls);
#endif
    }

    /// <summary>
    /// 启动泛型 Web 主机
    /// </summary>
    /// <remarks>未包含 Web 基础功能，需手动注册服务/中间件</remarks>
    /// <param name="options">配置选项</param>
    /// <param name="urls">默认 5000/5001 端口</param>
    public static void Run(LegacyRunOptions options, string urls = default)
    {
        Run<FakeStartup>(options, urls);
    }

    /// <summary>
    /// 启动泛型 Web 主机
    /// </summary>
    /// <remarks>未包含 Web 基础功能，需手动注册服务/中间件</remarks>
    /// <typeparam name="TStartup">启动 Startup 类</typeparam>
    /// <param name="options">配置选项</param>
    /// <param name="urls">默认 5000/5001 端口</param>
    public static void Run<TStartup>(LegacyRunOptions options, string urls = default)
        where TStartup : class
    {
        // 获取命令行参数
        var args = options.Args ?? Environment.GetCommandLineArgs().Skip(1).ToArray();

        var builder = Host.CreateDefaultBuilder(args);

        // 静默启动排除指定日志类名
        if (options.IsSilence && !options.SilenceLogging)
        {
            builder = builder.ConfigureLogging(logging =>
            {
                logging.AddFilter((provider, category, logLevel) =>
                {
                    return !SilenceExcludesOfLogCategoryName.Any(u => category.StartsWith(u));
                });
            });
        }

        // 添加自定义配置
        if (options.ActionConfigurationManager != null)
        {
            builder = builder.ConfigureAppConfiguration((context, configuration) =>
            {
                options.ActionConfigurationManager.Invoke(context.HostingEnvironment, configuration);
            });
        }

        // 配置 Web 主机
        builder = builder.ConfigureWebHostDefaults(webHostBuilder =>
        {
            webHostBuilder = webHostBuilder.Inject();

            // 解决部分主机不能正确读取 urls 参数命令问题
            var startUrls = !string.IsNullOrWhiteSpace(urls) ? urls : webHostBuilder.GetSetting(nameof(urls));

            // 自定义启动端口
            if (!string.IsNullOrWhiteSpace(startUrls))
            {
                webHostBuilder = webHostBuilder.UseUrls(startUrls);
            }

            // 配置服务
            if (options.ServiceComponents.Any())
            {
                webHostBuilder = webHostBuilder.ConfigureServices(services =>
                {
                    // 注册应用服务组件
                    foreach (var (componentType, opt) in options.ServiceComponents)
                    {
                        services.AddComponent(componentType, opt);
                    }
                });
            }

            // 配置中间件
            if (options.ApplicationComponents.Any())
            {
                webHostBuilder = webHostBuilder.Configure((context, app) =>
                {
                    // 注册应用中间件组件
                    foreach (var (componentType, opt) in options.ApplicationComponents)
                    {
                        app.UseComponent(context.HostingEnvironment, componentType, opt);
                    }
                });
            }

            // 解决 .NET5 项目必须配置 Startup 问题
            if (typeof(TStartup) != typeof(FakeStartup))
            {
                webHostBuilder = webHostBuilder.UseStartup<TStartup>();
            }

            // 调用自定义配置
            webHostBuilder = options?.ActionWebDefaultsBuilder?.Invoke(webHostBuilder) ?? webHostBuilder;
        });

        // 调用自定义配置
        builder = options?.ActionBuilder?.Invoke(builder) ?? builder;

        // 是否静默启动
        if (!options.IsSilence)
        {
            builder.Build().Run();
        }
        else
        {
            builder.Build().Start();
        }
    }

    /// <summary>
    /// 启动泛型通用主机
    /// </summary>
    /// <param name="options">配置选项</param>
    public static void Run(GenericRunOptions options)
    {
        // 获取命令行参数
        var args = options.Args ?? Environment.GetCommandLineArgs().Skip(1).ToArray();

        var builder = Host.CreateDefaultBuilder(args);

        // 静默启动排除指定日志类名
        if (options.IsSilence && !options.SilenceLogging)
        {
            builder = builder.ConfigureLogging(logging =>
            {
                logging.AddFilter((provider, category, logLevel) =>
                {
                    return !SilenceExcludesOfLogCategoryName.Any(u => category.StartsWith(u));
                });
            });
        }

        // 添加自定义配置
        if (options.ActionConfigurationManager != null)
        {
            builder = builder.ConfigureAppConfiguration((context, configuration) =>
            {
                options.ActionConfigurationManager.Invoke(context.HostingEnvironment, configuration);
            });
        }

        // 初始化框架
        builder = builder.Inject();

        // 配置服务
        if (options.ServiceComponents.Any())
        {
            builder = builder.ConfigureServices(services =>
            {
                // 注册应用服务组件
                foreach (var (componentType, opt) in options.ServiceComponents)
                {
                    services.AddComponent(componentType, opt);
                }
            });
        }

        // 调用自定义配置
        builder = options?.ActionBuilder?.Invoke(builder) ?? builder;

        // 是否静默启动
        if (!options.IsSilence)
        {
            builder.Build().Run();
        }
        else
        {
            builder.Build().Start();
        }
    }

#if !NET5_0
    /// <summary>
    /// 启动 WebApplication 主机
    /// </summary>
    /// <remarks>未包含 Web 基础功能，需手动注册服务/中间件</remarks>
    /// <param name="options">配置选项</param>
    /// <param name="urls">默认 5000/5001 端口</param>
    public static void Run(RunOptions options, string urls = default)
    {
        // 获取命令行参数
        var args = options.Args ?? Environment.GetCommandLineArgs().Skip(1).ToArray();

        // 初始化 WebApplicationBuilder
        var builder = (options.Options == null
            ? WebApplication.CreateBuilder(args)
            : WebApplication.CreateBuilder(options.Options));

        // 静默启动排除指定日志类名
        if (options.IsSilence && !options.SilenceLogging)
        {
            builder.Logging.AddFilter((provider, category, logLevel) =>
            {
                return !SilenceExcludesOfLogCategoryName.Any(u => category.StartsWith(u));
            });
        }

        // 添加自定义配置
        options.ActionConfigurationManager?.Invoke(builder.Environment, builder.Configuration);

        // 初始化框架
        builder.Inject();

        // 注册服应用务组件
        if (options.ServiceComponents.Any())
        {
            foreach (var (componentType, opt) in options.ServiceComponents)
            {
                builder.AddComponent(componentType, opt);
            }
        }

        // 解决部分主机不能正确读取 urls 参数命令问题
        var startUrls = !string.IsNullOrWhiteSpace(urls) ? urls : builder.Configuration[nameof(urls)];

        // 自定义启动端口
        if (!string.IsNullOrWhiteSpace(startUrls))
        {
            builder.WebHost.UseUrls(startUrls);
        }

        // 调用自定义配置
        options?.ActionBuilder?.Invoke(builder);

        // 初始化 WebApplication
        var app = builder.Build();

        // 注册应用中间件组件
        if (options.ApplicationComponents.Any())
        {
            foreach (var (componentType, opt) in options.ApplicationComponents)
            {
                app.UseComponent(app.Environment, componentType, opt);
            }
        }

        // 调用自定义配置
        options?.ActionConfigure?.Invoke(app);

        // 是否静默启动
        if (!options.IsSilence)
        {
            app.Run();
        }
        else
        {
            app.Start();
        }
    }
#endif
}