// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace System;

/// <summary>
/// 泛型主机方式配置选项
/// </summary>
[SuppressSniffer]
public class GenericRunOptions : IRunOptions
{
    /// <summary>
    /// 内部构造函数
    /// </summary>
    internal GenericRunOptions()
    {
    }

    /// <summary>
    /// 默认配置
    /// </summary>
    public static GenericRunOptions Default { get; } = new GenericRunOptions();

    /// <summary>
    /// 默认配置（带启动参数）
    /// </summary>
    public static GenericRunOptions Main(string[] args)
    {
        return Default.WithArgs(args);
    }

    /// <summary>
    /// 默认配置（静默启动）
    /// </summary>
    public static GenericRunOptions DefaultSilence { get; } = new GenericRunOptions().Silence();

    /// <summary>
    /// 默认配置（静默启动 + 启动参数）
    /// </summary>
    public static GenericRunOptions MainSilence(string[] args)
    {
        return DefaultSilence.WithArgs(args);
    }

    /// <summary>
    /// 配置 <see cref="IHostBuilder"/>
    /// </summary>
    /// <param name="configureAction">配置委托</param>
    /// <returns><see cref="GenericRunOptions"/></returns>
    public virtual GenericRunOptions ConfigureBuilder(Func<IHostBuilder, IHostBuilder> configureAction)
    {
        ActionBuilder = configureAction;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="configureAction"></param>
    /// <returns><see cref="GenericRunOptions"/></returns>
    public virtual GenericRunOptions ConfigureServices(Action<IServiceCollection> configureAction)
    {
        ActionServices = configureAction;
        return this;
    }

    /// <summary>
    /// 配置 <see cref="InjectOptions"/>
    /// </summary>
    /// <param name="configureAction"></param>
    /// <returns><see cref="RunOptions"/></returns>
    public virtual GenericRunOptions ConfigureInject(Action<IHostBuilder, InjectOptions> configureAction)
    {
        ActionInject = configureAction;
        return this;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public virtual GenericRunOptions AddComponent<TComponent>()
        where TComponent : class, IServiceComponent, new()
    {
        ServiceComponents.Add(typeof(TComponent), null);
        return this;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <typeparam name="TComponentOptions"></typeparam>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public virtual GenericRunOptions AddComponent<TComponent, TComponentOptions>(TComponentOptions options)
        where TComponent : class, IServiceComponent, new()
    {
        ServiceComponents.Add(typeof(TComponent), options);
        return this;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public virtual GenericRunOptions AddComponent(Type componentType, object options)
    {
        ServiceComponents.Add(componentType, options);
        return this;
    }

    /// <summary>
    /// 标识主机静默启动
    /// </summary>
    /// <remarks>不阻塞程序运行</remarks>
    /// <param name="silence">静默启动</param>
    /// <param name="logging">静默启动日志状态，默认 false</param>
    /// <returns></returns>
    public virtual GenericRunOptions Silence(bool silence = true, bool logging = false)
    {
        IsSilence = silence;
        SilenceLogging = logging;
        return this;
    }

    /// <summary>
    /// 设置进程启动参数
    /// </summary>
    /// <param name="args">启动参数</param>
    /// <returns></returns>
    public virtual GenericRunOptions WithArgs(string[] args)
    {
        Args = args;
        return this;
    }

    /// <summary>
    /// 自定义 <see cref="IHostBuilder"/> 委托
    /// </summary>
    internal Func<IHostBuilder, IHostBuilder> ActionBuilder { get; set; }

    /// <summary>
    /// 自定义 <see cref="IServiceCollection"/> 委托
    /// </summary>
    internal Action<IServiceCollection> ActionServices { get; set; }

    /// <summary>
    /// 自定义 <see cref="InjectOptions"/> 委托
    /// </summary>
    internal Action<IHostBuilder, InjectOptions> ActionInject { get; set; }

    /// <summary>
    /// 应用服务组件
    /// </summary>
    internal Dictionary<Type, object> ServiceComponents { get; set; } = new();

    /// <summary>
    /// 静默启动
    /// </summary>
    /// <remarks>不阻塞程序运行</remarks>
    internal bool IsSilence { get; private set; }

    /// <summary>
    /// 启用静默启动日志
    /// </summary>
    internal bool SilenceLogging { get; set; }

    /// <summary>
    /// 命令行参数
    /// </summary>
    internal string[] Args { get; set; }
}