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

using Furion;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
    public static new LegacyRunOptions Main(string[] args)
    {
        return Default.WithArgs(args);
    }

    /// <summary>
    /// 默认配置（静默启动）
    /// </summary>
    public static new LegacyRunOptions DefaultSilence { get; } = new LegacyRunOptions().Silence();

    /// <summary>
    /// 默认配置（静默启动 + 启动参数）
    /// </summary>
    public static new LegacyRunOptions MainSilence(string[] args)
    {
        return DefaultSilence.WithArgs(args);
    }

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
    /// 配置 <see cref="IWebHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public LegacyRunOptions ConfigureWebInject(Action<IWebHostBuilder, InjectOptions> configureAction)
    {
        ActionWebInject = configureAction;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="IHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public new LegacyRunOptions ConfigureBuilder(Func<IHostBuilder, IHostBuilder> configureAction)
    {
        return base.ConfigureBuilder(configureAction) as LegacyRunOptions;
    }

    /// <summary>
    /// 配置 <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="configureAction"></param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public new LegacyRunOptions ConfigureServices(Action<IServiceCollection> configureAction)
    {
        return base.ConfigureServices(configureAction) as LegacyRunOptions;
    }

    /// <summary>
    /// 配置 <see cref="IHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="LegacyRunOptions"/></returns>
    public new LegacyRunOptions ConfigureInject(Action<IHostBuilder, InjectOptions> configureAction)
    {
        return base.ConfigureInject(configureAction) as LegacyRunOptions;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public new LegacyRunOptions AddComponent<TComponent>()
        where TComponent : class, IServiceComponent, new()
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
    public new LegacyRunOptions AddComponent<TComponent, TComponentOptions>(TComponentOptions options)
        where TComponent : class, IServiceComponent, new()
    {
        return base.AddComponent<TComponent, TComponentOptions>(options) as LegacyRunOptions;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public new LegacyRunOptions AddComponent(Type componentType, object options)
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
    /// 添加 IWebHostBuilder 组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public LegacyRunOptions AddWebComponent<TComponent>()
        where TComponent : class, IWebComponent, new()
    {
        WebComponents.Add(typeof(TComponent), null);
        return this;
    }

    /// <summary>
    /// 添加 IWebHostBuilder 组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <typeparam name="TComponentOptions"></typeparam>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public LegacyRunOptions AddWebComponent<TComponent, TComponentOptions>(TComponentOptions options)
        where TComponent : class, IWebComponent, new()
    {
        WebComponents.Add(typeof(TComponent), options);
        return this;
    }

    /// <summary>
    /// 添加 IWebHostBuilder 组件
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public LegacyRunOptions AddWebComponent(Type componentType, object options)
    {
        WebComponents.Add(componentType, options);
        return this;
    }

    /// <summary>
    /// 标识主机静默启动
    /// </summary>
    /// <remarks>不阻塞程序运行</remarks>
    /// <param name="silence">静默启动</param>
    /// <param name="logging">静默启动日志状态，默认 false</param>
    /// <returns></returns>
    public new LegacyRunOptions Silence(bool silence = true, bool logging = false)
    {
        return base.Silence(silence, logging) as LegacyRunOptions;
    }

    /// <summary>
    /// 设置进程启动参数
    /// </summary>
    /// <param name="args">启动参数</param>
    /// <returns></returns>
    public new LegacyRunOptions WithArgs(string[] args)
    {
        return base.WithArgs(args) as LegacyRunOptions;
    }

    /// <summary>
    /// 自定义 <see cref="InjectOptions"/> 委托
    /// </summary>
    internal Action<IWebHostBuilder, InjectOptions> ActionWebInject { get; set; }

    /// <summary>
    /// 自定义 <see cref="IWebHostBuilder"/> 委托
    /// </summary>
    internal Func<IWebHostBuilder, IWebHostBuilder> ActionWebDefaultsBuilder { get; set; }

    /// <summary>
    /// 应用中间件组件
    /// </summary>
    internal Dictionary<Type, object> ApplicationComponents { get; set; } = new();

    /// <summary>
    /// IWebHostBuilder 组件
    /// </summary>
    internal Dictionary<Type, object> WebComponents { get; set; } = new();
}