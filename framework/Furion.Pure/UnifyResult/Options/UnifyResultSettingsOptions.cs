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

namespace Furion.UnifyResult;

/// <summary>
/// 规范化配置选项
/// </summary>
public sealed class UnifyResultSettingsOptions : IConfigurableOptions<UnifyResultSettingsOptions>
{
    /// <summary>
    /// 设置返回 200 状态码列表
    /// <para>默认：401，403，如果设置为 null，则标识所有状态码都返回 200 </para>
    /// </summary>
    public int[] Return200StatusCodes { get; set; }

    /// <summary>
    /// 适配（篡改）Http 状态码（只支持短路状态码，比如 401，403，500 等）
    /// </summary>
    public int[][] AdaptStatusCodes { get; set; }

    /// <summary>
    /// 是否支持 MVC 控制台规范化处理
    /// </summary>
    public bool? SupportMvcController { get; set; }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(UnifyResultSettingsOptions options, IConfiguration configuration)
    {
        options.Return200StatusCodes ??= new[] { 401, 403 };
        options.SupportMvcController ??= false;
    }
}