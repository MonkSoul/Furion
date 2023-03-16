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

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Furion;

/// <summary>
/// Inject 配置选项
/// </summary>
public sealed class InjectOptions
{
    /// <summary>
    /// 外部程序集名称
    /// </summary>
    public string AssemblyName { get; set; }

    /// <summary>
    /// 是否自动注册 BackgroundService
    /// </summary>
    public bool AutoRegisterBackgroundService { get; set; } = true;

    /// <summary>
    /// 配置 ConfigurationScanDirectories
    /// </summary>
    /// <param name="directories"></param>
    public void ConfigurationScanDirectories(params string[] directories)
    {
        InternalConfigurationScanDirectories = directories ?? Array.Empty<string>();
    }

    /// <summary>
    /// 配置 IgnoreConfigurationFiles
    /// </summary>
    /// <param name="files"></param>
    public void IgnoreConfigurationFiles(params string[] files)
    {
        InternalIgnoreConfigurationFiles = files ?? Array.Empty<string>();
    }

    /// <summary>
    /// 配置 ConfigureAppConfiguration
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configure)
    {
        AppConfigurationConfigure = configure;
    }

    /// <summary>
    /// 配置 ConfigureAppConfiguration（Web）
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureWebAppConfiguration(Action<WebHostBuilderContext, IConfigurationBuilder> configure)
    {
        WebAppConfigurationConfigure = configure;
    }

    /// <summary>
    /// 配置 ConfigureServices
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureServices(Action<HostBuilderContext, IServiceCollection> configure)
    {
        ServicesConfigure = configure;
    }

    /// <summary>
    /// 配置 ConfigureServices（Web）
    /// </summary>
    /// <param name="configure"></param>
    public void ConfigureWebServices(Action<WebHostBuilderContext, IServiceCollection> configure)
    {
        WebServicesConfigure = configure;
    }

    /// <summary>
    /// 配置配置文件扫描目录
    /// </summary>
    internal static IEnumerable<string> InternalConfigurationScanDirectories { get; private set; } = Array.Empty<string>();

    /// <summary>
    /// 配置配置文件忽略注册文件
    /// </summary>
    internal static IEnumerable<string> InternalIgnoreConfigurationFiles { get; private set; } = Array.Empty<string>();

    /// <summary>
    /// AppConfiguration 配置
    /// </summary>
    internal static Action<HostBuilderContext, IConfigurationBuilder> AppConfigurationConfigure { get; private set; }

    /// <summary>
    /// AppConfiguration 配置（Web）
    /// </summary>
    internal static Action<WebHostBuilderContext, IConfigurationBuilder> WebAppConfigurationConfigure { get; private set; }

    /// <summary>
    /// Services 配置
    /// </summary>
    internal static Action<HostBuilderContext, IServiceCollection> ServicesConfigure { get; private set; }

    /// <summary>
    /// Services 配置（Web）
    /// </summary>
    internal static Action<WebHostBuilderContext, IServiceCollection> WebServicesConfigure { get; private set; }
}