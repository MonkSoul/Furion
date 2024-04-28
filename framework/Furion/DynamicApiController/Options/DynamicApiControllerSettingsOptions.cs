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

using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Furion.DynamicApiController;

/// <summary>
/// 动态接口控制器配置
/// </summary>
public sealed class DynamicApiControllerSettingsOptions : IConfigurableOptions<DynamicApiControllerSettingsOptions>
{
    /// <summary>
    /// 默认路由前缀
    /// </summary>
    public string DefaultRoutePrefix { get; set; }

    /// <summary>
    /// 默认请求谓词
    /// </summary>
    [Required]
    public string DefaultHttpMethod { get; set; }

    /// <summary>
    /// 默认模块名称
    /// </summary>
    public string DefaultModule { get; set; }

    /// <summary>
    /// 小写路由
    /// </summary>
    public bool? LowercaseRoute { get; set; }

    /// <summary>
    /// 小驼峰命名（首字符小写）
    /// </summary>
    public bool? AsLowerCamelCase { get; set; }

    /// <summary>
    /// 保留行为名称谓词
    /// </summary>
    public bool? KeepVerb { get; set; }

    /// <summary>
    /// 保留名称
    /// </summary>
    public bool? KeepName { get; set; }

    /// <summary>
    /// 骆驼命名分隔符
    /// </summary>
    public string CamelCaseSeparator { get; set; }

    /// <summary>
    /// 版本号分隔符
    /// </summary>
    [Required]
    public string VersionSeparator { get; set; }

    /// <summary>
    /// 版本号在前面
    /// </summary>
    public bool? VersionInFront { get; set; }

    /// <summary>
    /// 模型转查询参数（只有GET、HEAD请求有效）
    /// </summary>
    public bool? ModelToQuery { get; set; }

    /// <summary>
    /// 支持Mvc控制器处理
    /// </summary>
    public bool? SupportedMvcController { get; set; }

    /// <summary>
    /// 配置参数 [FromQuery] 化，默认 false ([FromRoute])
    /// </summary>
    public bool? UrlParameterization { get; set; }

    /// <summary>
    /// 被舍弃的控制器名称前后缀
    /// </summary>
    public string[] AbandonControllerAffixes { get; set; }

    /// <summary>
    /// 被舍弃的行为名称前后缀
    /// </summary>
    public string[] AbandonActionAffixes { get; set; }

    /// <summary>
    /// 复写默认配置路由规则配置
    /// </summary>
    public object[][] VerbToHttpMethods { get; set; }

    /// <summary>
    /// 默认区域
    /// </summary>
    public string DefaultArea { get; set; }

    /// <summary>
    /// 强制携带路由前缀，即使使用 [Route] 重写
    /// </summary>
    public bool? ForceWithRoutePrefix { get; set; }

    /// <summary>
    /// 默认基元参数绑定方式
    /// </summary>
    public string DefaultBindingInfo { get; set; }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(DynamicApiControllerSettingsOptions options, IConfiguration configuration)
    {
        options.DefaultRoutePrefix ??= "api";
        options.DefaultHttpMethod ??= "POST";
        options.LowercaseRoute ??= true;
        options.AsLowerCamelCase ??= false;
        options.KeepVerb ??= false;
        options.KeepName ??= false;
        options.CamelCaseSeparator ??= "-";
        options.VersionSeparator ??= "v";
        options.VersionInFront ??= true;
        options.ModelToQuery ??= false;
        options.SupportedMvcController ??= false;
        options.ForceWithRoutePrefix ??= false;
        options.AbandonControllerAffixes ??= new string[]
        {
                "AppServices",
                "AppService",
                "ApiController",
                "Controller",
                "Services",
                "Service"
        };
        options.AbandonActionAffixes ??= new string[]
        {
                "Async"
        };
        DefaultBindingInfo ??= "route";
    }
}