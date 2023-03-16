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
        options.VersionSeparator ??= "@";
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
    }
}