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