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

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace System;

/// <summary>
/// Web 泛型主机方式配置选项
/// </summary>
[SuppressSniffer]
public sealed class LegacyRunOptions : GenericRunOptions
{
    /// <summary>
    /// 内部构造函数
    /// </summary>
    internal LegacyRunOptions()
        : base()
    {
    }

    /// <summary>
    /// 默认配置
    /// </summary>
    public static new LegacyRunOptions Default { get; } = new LegacyRunOptions();

    /// <summary>
    /// 默认配置（带启动参数）
    /// </summary>
    public static new LegacyRunOptions Main(string[] args) => Default.WithArgs(args);

    /// <summary>
    /// 默认配置（静默启动）
    /// </summary>
    public static new LegacyRunOptions DefaultSilence { get; } = new LegacyRunOptions().Silence();

    /// <summary>
    /// 默认配置（静默启动 + 启动参数）
    /// </summary>
    public static new LegacyRunOptions MainSilence(string[] args) => DefaultSilence.WithArgs(args);

    /// <summary>
    /// 配置 <see cref="IWebHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public LegacyRunOptions ConfigureWebDefaults(Func<IWebHostBuilder, IWebHostBuilder> configureAction)
    {
        ActionWebDefaultsBuilder = configureAction;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="IConfigurationBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public override LegacyRunOptions ConfigureConfiguration(Action<IHostEnvironment, IConfigurationBuilder> configureAction)
    {
        return base.ConfigureConfiguration(configureAction) as LegacyRunOptions;
    }

    /// <summary>
    /// 配置 <see cref="IHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public override LegacyRunOptions ConfigureBuilder(Func<IHostBuilder, IHostBuilder> configureAction)
    {
        return base.ConfigureBuilder(configureAction) as LegacyRunOptions;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public override LegacyRunOptions AddComponent<TComponent>()
    {
        return base.AddComponent<TComponent>() as LegacyRunOptions;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <typeparam name="TComponentOptions"></typeparam>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public override LegacyRunOptions AddComponent<TComponent, TComponentOptions>(TComponentOptions options)
    {
        return base.AddComponent<TComponent, TComponentOptions>(options) as LegacyRunOptions;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public override LegacyRunOptions AddComponent(Type componentType, object options)
    {
        return base.AddComponent(componentType, options) as LegacyRunOptions;
    }

    /// <summary>
    /// 添加应用中间件组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public LegacyRunOptions UseComponent<TComponent>()
        where TComponent : class, IApplicationComponent, new()
    {
        ApplicationComponents.Add(typeof(TComponent), null);
        return this;
    }

    /// <summary>
    /// 添加应用中间件组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <typeparam name="TComponentOptions"></typeparam>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public LegacyRunOptions UseComponent<TComponent, TComponentOptions>(TComponentOptions options)
        where TComponent : class, IApplicationComponent, new()
    {
        ApplicationComponents.Add(typeof(TComponent), options);
        return this;
    }

    /// <summary>
    /// 添加应用中间件组件
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public LegacyRunOptions UseComponent(Type componentType, object options)
    {
        ApplicationComponents.Add(componentType, options);
        return this;
    }

    /// <summary>
    /// 标识主机静默启动
    /// </summary>
    /// <remarks>不阻塞程序运行</remarks>
    /// <param name="silence">静默启动</param>
    /// <param name="logging">静默启动日志状态，默认 false</param>
    /// <returns></returns>
    public override LegacyRunOptions Silence(bool silence = true, bool logging = false)
    {
        return base.Silence(silence, logging) as LegacyRunOptions;
    }

    /// <summary>
    /// 设置进程启动参数
    /// </summary>
    /// <param name="args">启动参数</param>
    /// <returns></returns>
    public override LegacyRunOptions WithArgs(string[] args)
    {
        return base.WithArgs(args) as LegacyRunOptions;
    }

    /// <summary>
    /// 自定义 <see cref="IWebHostBuilder"/> 委托
    /// </summary>
    internal Func<IWebHostBuilder, IWebHostBuilder> ActionWebDefaultsBuilder { get; set; }

    /// <summary>
    /// 应用中间件组件
    /// </summary>
    internal Dictionary<Type, object> ApplicationComponents { get; set; } = new();
}