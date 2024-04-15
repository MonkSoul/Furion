// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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