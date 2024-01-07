// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;

namespace Furion.DependencyInjection;

/// <summary>
/// 依赖注入配置选项
/// </summary>
public sealed class DependencyInjectionSettingsOptions : IConfigurableOptions<DependencyInjectionSettingsOptions>
{
    /// <summary>
    /// 外部注册定义
    /// </summary>
    public ExternalService[] Definitions { get; set; }

    /// <summary>
    /// 后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(DependencyInjectionSettingsOptions options, IConfiguration configuration)
    {
        options.Definitions ??= Array.Empty<ExternalService>();
    }
}