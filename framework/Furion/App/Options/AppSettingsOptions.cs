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

using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Furion;

/// <summary>
/// 应用全局配置
/// </summary>
public sealed class AppSettingsOptions : IConfigurableOptions<AppSettingsOptions>
{
    /// <summary>
    /// 集成 MiniProfiler 组件
    /// </summary>
    public bool? InjectMiniProfiler { get; set; }

    /// <summary>
    /// 是否启用规范化文档
    /// </summary>
    public bool? InjectSpecificationDocument { get; set; }

    /// <summary>
    /// 是否启用引用程序集扫描
    /// </summary>
    public bool? EnabledReferenceAssemblyScan { get; set; }

    /// <summary>
    /// 外部程序集
    /// </summary>
    /// <remarks>扫描 dll 文件，如果是单文件发布，需拷贝放在根目录下</remarks>
    public string[] ExternalAssemblies { get; set; }

    /// <summary>
    /// 排除扫描的程序集
    /// </summary>
    public string[] ExcludeAssemblies { get; set; }

    /// <summary>
    /// 是否打印数据库连接信息到 MiniProfiler 中
    /// </summary>
    public bool? PrintDbConnectionInfo { get; set; }

    /// <summary>
    /// 是否输出原始 Sql 执行日志（ADO.NET）
    /// </summary>
    public bool? OutputOriginalSqlExecuteLog { get; set; }

    /// <summary>
    /// 配置支持的包前缀名
    /// </summary>
    public string[] SupportPackageNamePrefixs { get; set; }

    /// <summary>
    /// 【部署】二级虚拟目录
    /// </summary>
    public string VirtualPath { get; set; }

    /// <summary>
    /// 后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(AppSettingsOptions options, IConfiguration configuration)
    {
        // 非 Web 环境总是 false，如果是生产环境且不配置 InjectMiniProfiler，默认总是false，MiniProfiler 生产环境耗内存
        if (App.WebHostEnvironment == default
            || (App.HostEnvironment.IsProduction() && options.InjectMiniProfiler == null)) options.InjectMiniProfiler = false;
        else options.InjectMiniProfiler ??= true;

        options.InjectSpecificationDocument ??= true;
        options.EnabledReferenceAssemblyScan ??= false;
        options.ExternalAssemblies ??= Array.Empty<string>();
        options.ExcludeAssemblies ??= Array.Empty<string>();
        options.PrintDbConnectionInfo ??= true;
        options.OutputOriginalSqlExecuteLog ??= true;
        options.SupportPackageNamePrefixs ??= Array.Empty<string>();
        options.VirtualPath ??= string.Empty;
    }
}